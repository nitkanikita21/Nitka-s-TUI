using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using CS_TUI.UI;
using CS_TUI.UI.Graphics;
using CS_TUI.UI.Graphics.Elements;
using Microsoft.VisualBasic;

namespace CS_TUI
{
    class Program
    {
        public static Window window = new Window(
            "View Directory", ConsoleColor.DarkBlue,
            h:46, w:100
            );

        public static string pathToCurrentDir;

        private static SelectList list;
        private static List<string> itemsFolder = new List<string>();
        private static Text name;
        private static Text path;
        private static Text size;
        
        private static List<DriveInfo> allDrives = new List<DriveInfo>();
        private static int indexDriver;
        
        static void Main(string[] args)
        {
            UICore.init("File Manager");

            foreach (var driveInfo in DriveInfo.GetDrives())
            {
                if(driveInfo.IsReady)
                    allDrives.Add(driveInfo);
            }

            pathToCurrentDir = allDrives.Last().Name;

            #region Exit

            Button exit = new Button(new Vector2(0, -1), "Exit", ConsoleKey.Escape, ConsoleColor.Gray, back:ConsoleColor.DarkGray);
            exit.position = new Vector2(window.getWidth() - exit.getText().Length,-1);
            exit.pressButtonEvent += (key, button) =>
            {
                button.setText("Exiting...");
                exit.position = new Vector2(window.getWidth() - exit.getText().Length,-1);
                window.exit();
            };
            window.addElement(exit);

            #endregion

            Text controlLabel = new Text(new Vector2(2,40), "Move:[Up/Down] Open:[Enter] Delete:[Delete]",back:ConsoleColor.DarkBlue,color:ConsoleColor.DarkGreen);

            Text line = new Text(new Vector2(0,1), new string('=',window.getWidth()),back:ConsoleColor.DarkBlue,color:  ConsoleColor.DarkCyan);
            Text line2 = new Text(new Vector2(0,39), new string('=',window.getWidth()),back:ConsoleColor.DarkBlue,color:ConsoleColor.DarkCyan);
            Text line3 = new Text(new Vector2(0,41), new string('=',window.getWidth()),back:ConsoleColor.DarkBlue,color:ConsoleColor.DarkCyan);

            Button changeDisk = new Button(Vector2.Zero, "Change Dir", ConsoleKey.Spacebar);
            changeDisk.position = new Vector2(
                window.getWidth()-changeDisk.getText().Length-2,40
                );
            changeDisk.pressButtonEvent += (key, button) =>
            {
                indexDriver++;
                pathToCurrentDir = allDrives[indexDriver % allDrives.Count].Name;
                openDir();
            };

            #region File Data

            name = new Text(new Vector2(0, 42), "FILENAME" ,
                back: ConsoleColor.DarkBlue, color: ConsoleColor.DarkCyan);
            path = new Text(new Vector2(0, 43), "Path: PATH" ,
                back: ConsoleColor.DarkBlue, color: ConsoleColor.DarkCyan);
            size = new Text(new Vector2(0, 44), "SIZE" ,
                back: ConsoleColor.DarkBlue, color: ConsoleColor.DarkYellow);

            #endregion

            #region DirItems

            list = new SelectList(new Vector2(0, 1), width:window.getWidth(),heigth:38, selector: "> ",bg:ConsoleColor.DarkBlue, fgSelected:ConsoleColor.Cyan,fgUnselected:ConsoleColor.Cyan);

            list.selectItemEvent += (i, label) =>
            {
                updateDetails(i);
            };

            list.clickItemEvent += (i, label) =>
            {
                FileAttributes attr = File.GetAttributes(itemsFolder[i]);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    pathToCurrentDir = Path.GetFullPath(getLastFromPath(itemsFolder[i]), pathToCurrentDir);
                    openDir();
                    updateDetails(0);
                }
                else
                {
                    
                }
            };

            openDir();
            updateDetails(0);

            
            #endregion
            
            #region Add Element

            window.addElement(line);
            window.addElement(line2);
            window.addElement(line3);

            window.addElement(list);
            window.addElement(controlLabel);
            window.addElement(changeDisk);

            
            window.addElement(name);
            window.addElement(path);
            window.addElement(size);


            
            window.render();

            #endregion

            while (true)
            {
                ;
            }
        }

        public static void openDir()
        {
            list.clear();
            itemsFolder = new List<string>();

            itemsFolder.Add("../");
            list.addItem(new Text(Vector2.Zero, "../"));
            foreach (string directory in Directory.GetDirectories(pathToCurrentDir))
            {
                list.addItem(new Text(Vector2.Zero, "DIR "+Path.GetFileName(directory)));
                itemsFolder.Add(directory);
            }
            foreach (string file in Directory.GetFiles(pathToCurrentDir))
            {
                list.addItem(new Text(Vector2.Zero, Path.GetFileName(file)));
                itemsFolder.Add(file);
            }

            window.setTitle((allDrives[indexDriver % allDrives.Count].Name)+(Path.GetFileName(pathToCurrentDir) != "" ? " | "+Path.GetFileName(pathToCurrentDir) : ""));
            window.render();
        }

        public static void updateDetails(int i)
        {
            FileAttributes attr = File.GetAttributes(itemsFolder[i]);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                name.setText(new string(' ',name.getText().Length));
                name.render();
                name.setText(itemsFolder[i].Split(@"\").Last()); 
                    
                size.setText(new string(' ',size.getText().Length));
                size.render();
                size.setText(" ");
                    
            }
            else
            {
                name.setText(new string(' ',name.getText().Length));
                name.render();
                name.setText(Path.GetFileName(itemsFolder[i]));
                    
                long length = new System.IO.FileInfo(itemsFolder[i]).Length;
                    
                size.setText(new string(' ',size.getText().Length));
                size.render();
                size.setText(length.SizeSuffix());
            }
            name.render();
            path.setText(new string(' ',path.getText().Length));
            path.render();
            path.setText(itemsFolder[i]);
            path.render();
            size.render();
        }
        
        
        public static string getLastFromPath(string path)
        {
            return path.Split(@"\").Last();
        }
    }
}