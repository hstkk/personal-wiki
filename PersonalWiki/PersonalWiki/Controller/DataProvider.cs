﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PersonalWiki
{
    /// <summary>
    /// DataProvider class provides all LINQ queries
    /// </summary>
    class DataProvider : IDisposable
    {
        private Database db = null;

        /// <summary>
        /// try to connect to database
        /// </summary>
        public DataProvider()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabasePath) && File.Exists(Properties.Settings.Default.DatabasePath))
                {
                    string conn = string.Format("Data Source={0}", @Properties.Settings.Default.DatabasePath);
                    db = new Database(conn);
                }
                else
                    MessageBox.Show("Error, can't open database, database should be same directory with exe", "Error");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, can't open database, database should be same directory with exe", "Error");
            }
        }

        /// <summary>
        /// Gets all projects and pages for treeview
        /// </summary>
        public ObservableCollection<ProjectResult> GetProjectsTree()
        {
            try
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
            catch (Exception e)
            {
                return new ObservableCollection<ProjectResult>(null);
            }
        }

        /// <summary>
        /// Get pages by project id
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="id"></param>
        /// <returns>PageResult object in other words page title and id</returns>
        public ObservableCollection<PageResult> GetPages(int id)
        {
            try
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
            catch (Exception e)
            {
                return new ObservableCollection<PageResult>(null);
            }
        }

        /// <summary>
        /// Get page by page id
        /// </summary>
        /// <param name="id">Page id</param>
        /// <returns>PageResult2 object in other words page title and if revisions exists returns revision text and revision timestamp</returns>
        public ObservableCollection<PageResult2> GetPage(int id)
        {
            try
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
                            Title = p.PageTitle
                        };
                return new ObservableCollection<PageResult2>(page.Take(1));
            }
            catch (Exception e) {
                return new ObservableCollection<PageResult2>(null);
            }
        }

        /// <summary>
        /// Get revisions by page id
        /// </summary>
        /// <param name="id">Page id</param>
        /// <returns>Revision object in other words revision text and timestamp</returns>
        public ObservableCollection<Model.Revision> GetRevisions(int id)
        {
            try
            {
                var revision =
                    from r in db.Revision
                    orderby r.RevisionTimestamp descending
                    where r.PageId == id
                    select new Model.Revision
                    {
                        Date = r.RevisionTimestamp,
                        Text = r.RevisionText
                    };
                return new ObservableCollection<Model.Revision>(revision);
            }
            catch (Exception e)
            {
                return new ObservableCollection<Model.Revision>(null);
            }
        }

        /// <summary>
        /// Get tab header title by page id
        /// </summary>
        /// <param name="id">Page id</param>
        /// <returns>title string</returns>
        public string GetPageTabHeader(int id)
        {
            try
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
            }
            catch (Exception e) { }
            return string.Empty;
        }

        /// <summary>
        /// Updates pages title
        /// </summary>
        /// <param name="id">Page id</param>
        /// <param name="title">New title</param>
        /// <returns>true if update was succesful</returns>
        public bool updateTitle(int id, string title)
        {
            bool success = false;
            try
            {
                var page =
                    from p in db.Page
                    where p.PageId == id
                    select p;
                page.Single().PageTitle = title;
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, can't update title");
            }
            return success;
        }

        /// <summary>
        /// Adds new revision
        /// </summary>
        /// <param name="id">Page id</param>
        /// <param name="text">Revision text</param>
        /// <returns>true if add was succesful</returns>
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
                MessageBox.Show("Error can't add new revision", "Error");
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
        /// Returns all pages where page title or revision text contains keyword
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>PageResult2 object in other words page title, id, revision text and revision timestamp</returns>
        public ObservableCollection<PageResult2> FindPage(string keyword)
        {
            try
            {
            var page =
                from p in db.Page
                join r in db.Revision
                on p.PageId equals r.PageId
                orderby r.RevisionTimestamp descending
                where r.RevisionText.Contains(keyword) ||
                p.PageTitle.Contains(keyword)
                select new PageResult2
                {
                    Id = p.PageId,
                    Title = p.PageTitle,
                    Text = r.RevisionText,
                    Date = r.RevisionTimestamp
                };
            return new ObservableCollection<PageResult2>(page);
            }
            catch (Exception e)
            {
                return new ObservableCollection<PageResult2>(null);
            }
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

        /// <summary>
        /// Imports new page to database
        /// </summary>
        /// <param name="title">Pages name</param>
        /// <param name="projectId">Projects id</param>
        /// <param name="text">Revision text</param>
        /// <returns>true if added succesfully</returns>
        public bool importPage(string title, int projectId, string text)
        {
            bool success = false;
            try
            {
                Page p = new Page
                {
                    PageTitle = title,
                    ProjectId = projectId
                };
                db.Page.InsertOnSubmit(p);
                db.SubmitChanges();
                var page =
                    from pa in db.Page
                    where pa.ProjectId == projectId &&
                    pa.PageTitle == title
                    orderby pa.PageId descending
                    select pa;
                if(addRevision(page.Single().PageId, text))
                    success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error can't import new page", "Error");
            }
            return success;
        }

        /// <summary>
        /// Returns all projects
        /// </summary>
        /// <returns>PageResult object in other words project title and id</returns>
        private ObservableCollection<PageResult> GetProjects()
        {
            try
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
            catch (Exception e) {
                return new ObservableCollection<PageResult>(null);
            }
        }

        /// <summary>
        /// Checks if any projects exists
        /// </summary>
        /// <returns>true if any projects exists</returns>
        public bool ProjectExists()
        {
            bool exist = false;
            try
            {
            var projects =
                from p in db.Project
                select p;
            if (!projects.Count().Equals(0))
                exist = true;
            }
            catch (Exception e) { }
            return exist;
        }

        /// <summary>
        /// Checks if any projects exists by project title
        /// </summary>
        /// <param name="projectTitle">project title</param>
        /// <returns>true if any projects exists</returns>
        public bool ProjectExists(string projectTitle)
        {
            bool exist = false;
            try
            {
            var projects =
                from p in db.Project
                where p.ProjectTitle.Equals(projectTitle)
                select p;
            if (!projects.Count().Equals(0))
                exist = true;
            }
            catch (Exception e) { }
            return exist;
        }

        /// <summary>
        /// Checks if any revisions exists
        /// </summary>
        /// <returns>true if any revisions exists</returns>
        public bool RevisionsExists()
        {
            bool exist = false;
            try
            {
            var revisions =
                from r in db.Revision
                select r;
            if (!revisions.Count().Equals(0))
                exist = true;
            }
            catch (Exception e) { }
            return exist;
        }

        /// <summary>
        /// Delete page and its revisions by page id
        /// </summary>
        /// <param name="id">Page id</param>
        /// <returns>true, if delete is succesful</returns>
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
        /// Delete project and pages belonging to project
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>true, if delete is succesful</returns>
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
        /// Get pages project id by page id
        /// </summary>
        /// <param name="id">page id</param>
        /// <returns>projects id</returns>
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

        /// <summary>
        /// Checks if page exist
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="pageTitle">page title</param>
        /// <returns>true if page exists</returns>
        public bool PageExists(int projectId, string pageTitle)
        {
            bool exist = false;
            try
            {
            var pages =
                from p in db.Page
                where p.PageTitle.Equals(pageTitle) &&
                p.ProjectId.Equals(projectId)
                select p;
            if (!pages.Count().Equals(0))
                exist = true;
            }
            catch (Exception e) { }
            return exist;
        }

        //todo: create db
/*        public bool createDatabase()
        {
            bool success = false;
            try
            {
                string conn = string.Format("Data Source={0}", @Properties.Settings.Default.DatabasePath);
                db = new Database(conn);
                if (!File.Exists(DatabasePath.Path))
                {
                    db.CreateDatabase();
                    success = true;
                }
            }
            catch (Exception e) { }
            return success;
        }*/

        /// <summary>
        /// Disposes database object and calls garbage collection (this method allows to use using statement)
        /// </summary>
        public void Dispose()
        {
            try
            {
                db.Dispose();
                db = null;
                GC.SuppressFinalize(this);
            }
            catch (Exception e) { }
        }
    }
}
