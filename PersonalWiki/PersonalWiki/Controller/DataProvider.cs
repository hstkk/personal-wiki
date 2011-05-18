using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

//using System.Data;


namespace PersonalWiki
{
    class DataProvider : IDisposable
    {
/*        string strConn = string.Format("Data Source={0}", DatabasePath.Path);

        IDbConnection conn = new System.Data.SqlServerCe.SqlCeConnection(strConStr);

        Database db = new Database(conn);*/
        Database db = new Database(@"Data Source=C:\Users\Sami\Desktop\työnalla\Database.sdf");
//        Database db = new Database();

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ProjectResult> GetProjectsTree()
        {
            var projects =
                from p in db.Project
                orderby p.ProjectTitle ascending
                select new ProjectResult
                {
                    Id = p.ProjectId,
                    Title = p.ProjectTitle,
                    Pages = GetPages(p.ProjectId)
                };
            return new ObservableCollection<ProjectResult>(projects);
        }

        //if page is archived||if page is trash
        //todo:Error Getting Page||Create new page
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObservableCollection<PageResult> GetPages(int id)
        {
            var pages =
                from p in db.Page
                where p.ProjectId == id
                orderby p.PageTitle
                select new PageResult
                {
                    Id = p.PageId,
                    Title = p.PageTitle
                };
            return new ObservableCollection<PageResult>(pages);
        }

        public ObservableCollection<PageResult2> GetPage(int id)
        {
            var page =
                from p in db.Page
                join r in db.Revision
                on p.PageId equals r.PageId
                orderby r.RevisionTimestamp descending
                where p.PageId == id
                select new PageResult2
                {
                    Title = p.PageTitle,
                    Text = r.RevisionText,
                    Date = r.RevisionTimestamp
                };
            if(page.Count().Equals(0))
                page=
                    from p in db.Page
                    where p.PageId == id
                    select new PageResult2
                    {
                        Title = p.PageTitle/*,
                        _Archived = p.PageArchived.Value,
                        _Trash = p.PageTrash.Value*/
                    };

            return new ObservableCollection<PageResult2>(page.Take(1));
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Model.Revision> GetRevisions(int id)
        {
            var revision =
                from  r in db.Revision
                orderby r.RevisionTimestamp descending
                where r.PageId == id
                select new Model.Revision
                {
                    Date = r.RevisionTimestamp,
                    Text = r.RevisionText
                };
            return new ObservableCollection<Model.Revision>(revision);
        }

        public string GetPageTabHeader(int id)
        {
            var header =
                from p in db.Page
                join pr in db.Project
                on p.ProjectId equals pr.ProjectId
                where p.PageId == id
                select new PageResult
                {
                    Title = p.PageTitle+" – "+pr.ProjectTitle
                };
            if (header.ToArray().Count() == 1 && header.ToArray()[0].Title != null)
                return header.ToArray()[0].Title;
            return string.Empty;
        }

        public bool updateTitle(int id, string title)
        {
            try
            {
                var page =
                    from p in db.Page
                    where p.PageId == id
                    select p;
                page.Single().PageTitle = title;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                //todo:error handling
            }
            //todo:jos onnistuus
            return false;
        }

        public bool addRevision(int id, string text)
        {
            bool success = false;
            Revision r = new Revision
            {
                PageId = id,
                RevisionText = text,
                RevisionTimestamp = DateTime.Now
            };
            db.Revision.InsertOnSubmit(r);
            try
            {
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                //todo:error handling
            }
            return success;
        }

        /// <summary>
        /// Adds new project to database
        /// </summary>
        /// <param name="title">Projects name</param>
        /// <returns>true if added succesfully</returns>
        public bool addProject(string title)
        {
            bool success = false;
            Project p = new Project
            {
                ProjectTitle = title
            };
            db.Project.InsertOnSubmit(p);
            try
            {
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error can't add new project", "Error");
            }
            return success;
        }

        /// <summary>
        /// Adds new page to database
        /// </summary>
        /// <param name="title">Pages name</param>
        /// <param name="projectId">Projects id</param>
        /// <returns>true if added succesfully</returns>
        public bool addPage(string title, int projectId)
        {
            bool success = false;
            Page p = new Page
            {
                PageTitle = title,
                ProjectId = projectId
            };
            db.Page.InsertOnSubmit(p);
            try
            {
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error can't add new page", "Error");
            }
            return success;
        }

        private ObservableCollection<PageResult> GetProjects()
        {
            var projects =
                from p in db.Project
                orderby p.ProjectTitle
                select new PageResult
                {
                    Id = p.ProjectId,
                    Title = p.ProjectTitle
                };
            return new ObservableCollection<PageResult>(projects);
        }

        public bool ProjectExists()
        {
            bool exist = true;
            var projects =
                from p in db.Project
                select p;
            if (projects.Count().Equals(0))
                exist = false;
            return exist;
        }

        public bool ProjectExists(string projectTitle)
        {
            bool exist = true;
            var projects =
                from p in db.Project
                where p.ProjectTitle.Equals(projectTitle)
                select p;
            if (projects.Count().Equals(0))
                exist = false;
            return exist;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool deletePage(int id)
        {
            bool success = false;
            try
            {
                var revision =
                  from r in db.Revision
                    where r.PageId == id
                    select r;
                db.Revision.DeleteAllOnSubmit(revision);
                var page =
                    from p in db.Page
                    where p.PageId == id
                    select p;
                db.Page.DeleteAllOnSubmit(page);
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, can't delete page", "Error!");
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool deleteProject(int id)
        {
            bool success = false;
            try
            {
                var pages =
                from p in db.Page
                    where p.ProjectId == id
                    select p;
                foreach (var page in pages)
                    deletePage(page.PageId);
                var project =
                    from p in db.Project
                    where p.ProjectId == id
                    select p;
                db.Project.DeleteAllOnSubmit(project);
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, can't delete project", "Error!");
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        public int GetProjectId(int id)
        {
            int projectId = -1;
            try
            {
                var page =
                    from p in db.Page
                    where p.PageId == id
                    select p;
                projectId = page.Single().ProjectId;
            }
            catch (Exception e) { }
            return projectId;
        }

        public bool PageExists(int projectId, string pageTitle)
        {
            bool exist = true;
            var pages =
                from p in db.Page
                where p.PageTitle.Equals(pageTitle) &&
                p.ProjectId.Equals(projectId)
                select p;
            if (pages.Count().Equals(0))
                exist = false;
            return exist;
        }

        //todo: create db, users removed from dbml
        public bool createDatabase()
        {
/*            if (!File.Exists(DatabasePath.Path))
            {
                db.CreateDatabase();
                //todo:jos onnistuus
            }*/
            return false;
        }

        public void Dispose()
        {
            db.Dispose();
            db = null;
            GC.SuppressFinalize(this);
        }
    }
}
