﻿using System;
using System.Linq;
using EnvDTE;

namespace BuildVision.Helpers
{
    public static class ProjectItemExtensions
    {
        public static bool ProjectItemIsDirty(this ProjectItem projectItem)
        {
            if (projectItem.IsDirty)
            {
                return true;
            }

            if (projectItem.ProjectItems != null && projectItem.ProjectItems.Cast<ProjectItem>().Any(ProjectItemIsDirty))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find file in projects collection.
        /// </summary>
        /// <param name="items">Projects collection.</param>
        /// <param name="filePath">File path, relative to the <paramref name="items"/> root.</param>
        /// <returns>The found file or <c>null</c>.</returns>
        public static ProjectItem FindProjectItem(this ProjectItems items, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Argument `filePath` is null or empty.", "filePath");
            }

            int backslashIndex = filePath.IndexOf("\\", StringComparison.Ordinal);
            bool findFolder = backslashIndex != -1;
            if (findFolder)
            {
                string folderName = filePath.Substring(0, backslashIndex);
                foreach (ProjectItem item in items)
                {
                    if (item.Kind != Constants.vsProjectItemKindVirtualFolder &&
                        item.Kind != Constants.vsProjectItemKindPhysicalFolder)
                    {
                        continue;
                    }

                    if (folderName == item.Name)
                    {
                        string nextpath = filePath.Substring(backslashIndex + 1);
                        return FindProjectItem(item.ProjectItems, nextpath);
                    }
                }
            }
            else
            {
                string fileName = filePath;
                foreach (ProjectItem item in items)
                {
                    if (item.Kind != Constants.vsProjectItemKindPhysicalFile)
                    {
                        continue;
                    }

                    if (item.Name == fileName)
                    {
                        return item;
                    }

                    // Nested item, e.g. Default.aspx or MainWindow.xaml.
                    if (item.ProjectItems.Count > 0)
                    {
                        var childItem = FindProjectItem(item.ProjectItems, fileName);
                        if (childItem != null)
                        {
                            return childItem;
                        }
                    }
                }
            }

            return null;
        }
    }
}
