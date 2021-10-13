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

            Button exit = new Button(new Vector2(0, -1), ConsoleKey.Escape);
            exit.setText("Exit");
            exit.setBackground(ConsoleColor.DarkGray);
            exit.setColor(ConsoleColor.Black);
            exit.setPosition(new Vector2(window.getWidth() - exit.getText().Length,-1));
            exit.pressButtonEvent += (key, button) =>
            {
                button.setText("Exiting...");
                exit.setPosition(new Vector2(window.getWidth() - exit.getText().Length,-1));
                window.exit();
            };
            window.addElement(exit);

            #endregion

            #region Labels

            Text controlLabel = new Text(new Vector2(2, 40));
            controlLabel.setText("Move:[Up/Down] Open:[Enter] Delete:[Delete]");
            controlLabel.setBackground(ConsoleColor.DarkBlue);
            controlLabel.setColor(ConsoleColor.DarkGreen);

            Text line = new Text(new Vector2(0, 1));
            line.setText(new string('=', window.getWidth()));
            line.setBackground(ConsoleColor.DarkBlue);
            line.setColor(ConsoleColor.DarkCyan);
            
            Text line2 = new Text(new Vector2(0, 39));
            line2.setText(new string('=', window.getWidth()));
            line2.setBackground(ConsoleColor.DarkBlue);
            line2.setColor(ConsoleColor.DarkCyan);
            
            Text line3 = new Text(new Vector2(0, 41));
            line3.setText(new string('=', window.getWidth()));
            line3.setBackground(ConsoleColor.DarkBlue);
            line3.setColor(ConsoleColor.DarkCyan);

            #endregion
            
            Button changeDisk = new Button(Vector2.Zero, ConsoleKey.Spacebar);
            changeDisk.setText("Change Dir");
            changeDisk.setBackground(ConsoleColor.DarkBlue);
            changeDisk.setColor(ConsoleColor.DarkCyan);
            changeDisk.setPosition(new Vector2(
                window.getWidth()-changeDisk.getText().Length-2,40
            ));
            changeDisk.pressButtonEvent += (key, button) =>
            {
                indexDriver++;
                pathToCurrentDir = allDrives[indexDriver % allDrives.Count].Name;
                openDir();
            };

            #region File Data

            name = new Text(new Vector2(0, 42));
            name.setColor(ConsoleColor.DarkCyan);
            name.setBackground(ConsoleColor.DarkBlue);
            name.setText("FILENAME");
            
            path = new Text(new Vector2(0, 43));
            path.setColor(ConsoleColor.DarkCyan);
            path.setBackground(ConsoleColor.DarkBlue);
            path.setText("FILENAME");
            
            size = new Text(new Vector2(0, 44));
            size.setColor(ConsoleColor.DarkYellow);
            size.setBackground(ConsoleColor.DarkBlue);
            size.setText("FILENAME");

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
            
            var root = new Text(Vector2.Zero);
            root.setText("../");
            
            list.addItem(root);
            foreach (string directory in Directory.GetDirectories(pathToCurrentDir))
            {
                var dir = new Text(Vector2.Zero);
                dir.setText("DIR " + Path.GetFileName(directory));
                
                list.addItem(dir);
                itemsFolder.Add(directory);
            }
            foreach (string file in Directory.GetFiles(pathToCurrentDir))
            {
                var f = new Text(Vector2.Zero);
                f.setText(Path.GetFileName(file));
                
                list.addItem(f);
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