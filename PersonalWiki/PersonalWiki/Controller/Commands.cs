﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PersonalWiki
{
    /// <summary>
    /// Creates new custom commands, when get method is executed if RoutedUICommand object doesn't exist object will be created and object is returned
    /// </summary>
    static class Commands
    {
        private static RoutedUICommand newPage;
        public static ICommand NewPage
        {
            get
            {
                if (newPage == null)
                    newPage = new RoutedUICommand("New Page", "NewPage", typeof(Commands));
                return newPage;
            }
        }

        private static RoutedUICommand newProject;
        public static ICommand NewProject
        {
            get
            {
                if (newProject == null)
                    newProject = new RoutedUICommand("New Project", "NewProject", typeof(Commands));
                return newProject;
            }
        }

        private static RoutedUICommand showPage;
        public static ICommand ShowPage
        {
            get
            {
                if (showPage == null)
                    showPage = new RoutedUICommand("Show Page", "ShowPage", typeof(Commands));
                return showPage;
            }
        }

        private static RoutedUICommand closeTab;
        public static ICommand CloseTab
        {
            get
            {
                if (closeTab == null)
                    closeTab = new RoutedUICommand("Close tab", "CloseTab", typeof(Commands));
                return closeTab;
            }
        }

        
        private static RoutedUICommand showRevisions;
        public static ICommand ShowRevisions
        {
            get
            {
                if (showRevisions == null)
                    showRevisions = new RoutedUICommand("Show revisions", "ShowRevisions", typeof(Commands));
                return showRevisions;
            }
        }

        private static RoutedUICommand deleteProject;
        public static ICommand DeleteProject
        {
            get
            {
                if (deleteProject == null)
                    deleteProject = new RoutedUICommand("Delete project", "DeleteProject", typeof(Commands));
                return deleteProject;
            }
        }

        private static RoutedUICommand importPage;
        public static ICommand ImportPage
        {
            get
            {
                if (importPage == null)
                    importPage = new RoutedUICommand("Import page", "ImportPage", typeof(Commands));
                return importPage;
            }
        }

        private static RoutedUICommand exportTxt;
        public static ICommand ExportTxt
        {
            get
            {
                if (exportTxt == null)
                    exportTxt = new RoutedUICommand("Export txt", "ExportTxt", typeof(Commands));
                return exportTxt;
            }
        }

        private static RoutedUICommand exportHtml;
        public static ICommand ExportHtml
        {
            get
            {
                if (exportHtml == null)
                    exportHtml = new RoutedUICommand("Export html", "ExportHtml", typeof(Commands));
                return exportHtml;
            }
        }
    }
}
