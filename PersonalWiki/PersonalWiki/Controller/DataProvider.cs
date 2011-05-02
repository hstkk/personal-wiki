using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PersonalWiki
{
    class DataProvider : IDisposable
    {
        Database db = new Database();

        //if project is archived||if project is trash
        //todo:Error Getting Projects||Create new project
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ProjectResult> GetProjects()
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
        private ObservableCollection<PageResult> GetPages(int id)
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
                where p.PageId == id
                select new PageResult2
                {
                    Id = p.PageId,
                    Title = p.PageTitle,
                    Text = r.RevisionText,
                    Date = r.RevisionTimestamp/*,
                    _Archived = p.PageArchived.Value,
                    _Trash = p.PageTrash.Value*/
                };
            return new ObservableCollection<PageResult2>(page);
        }

        public string GetPageTabHeader(int id)
        {
            var header =
                from p in db.Page
                join pr in db.Project
                on p.ProjectId equals pr.ProjectId
                where p.PageId == id
                select new
                {
                    Header = p.PageTitle+" – "+pr.ProjectTitle
                }.Header;
            return header.ToString();
        }

        public bool updateTitle(int id, string title)
        {
            var page =
                from p in db.Page
                where p.PageId == id
                select p;
            page.Single().PageTitle = title;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                //todo:error handling
            }
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
