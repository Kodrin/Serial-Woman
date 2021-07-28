using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class TexturePixelatorWindow : EditorWindow
{
    public Texture2D inputTexture;
    public int targetWidth = 256;
    public int targetHeight = 256;

    public string outputPath = "/Texture/Pixelator/";
    
       [MenuItem ("Tools/Pixelator")]
        static void OpenWindow() 
        {
            //create window
            TexturePixelatorWindow window = EditorWindow.GetWindow<TexturePixelatorWindow>();
            window.Show();
        }
        
        protected void OnGUI(){
            EditorGUILayout.HelpBox("Set the source texture you want to pixelate, then press the \"Pixelate Texture\" button.", MessageType.None);
            
            using(var check = new EditorGUI.ChangeCheckScope())
            {
                inputTexture = (Texture2D)EditorGUILayout.ObjectField("Source Texture", inputTexture, typeof(Texture2D), false);
                targetWidth = EditorGUILayout.IntField("Target Width Resolution", targetWidth);
                targetHeight = EditorGUILayout.IntField("Target Height Resolution", targetHeight);
                outputPath = EditorGUILayout.TextField("Output Path", outputPath);
                // outputPath = FileField(outputPath);
                
                if(GUILayout.Button("Pixelate Texture"))
                {
                    if(inputTexture) Reduce();
                }

            }
            
            //warnings
            //tell the user what inputs are missing
            if(!inputTexture)
            {
                EditorGUILayout.HelpBox("You need a source texture in order to pixelate it", MessageType.Warning);
            }
            if(targetWidth < 64 || targetHeight < 64)
            {
                EditorGUILayout.HelpBox("Please set the target width and height to something higher", MessageType.Warning);
            }
            if(outputPath == null)
            {
                EditorGUILayout.HelpBox("Please specify a path for where to save the texture", MessageType.Warning);
            }
        }
        
        //https://www.ronja-tutorials.com/post/030-baking-shaders/
        protected string FileField(string path)
        {
            //allow the user to enter output file both as text or via file browser
            EditorGUILayout.LabelField("Extract Path");
            using (new GUILayout.HorizontalScope())
            {
                path = EditorGUILayout.TextField(path);
                if (GUILayout.Button("choose"))
                {
                    //set default values for directory, then try to override them with values of existing path
                    string directory = "Assets";
                    string fileName = "MaterialImage.png";
                    try
                    {
                        directory = Path.GetDirectoryName(path);
                        fileName = Path.GetFileName(path);
                    }
                    catch (ArgumentException)
                    {
                    }

                    string chosenFile = EditorUtility.SaveFilePanelInProject("Choose image file", fileName,
                        "png", "Please enter a file name to save the image to", directory);
                    if (!string.IsNullOrEmpty(chosenFile))
                    {
                        path = chosenFile;
                    }

                    //repaint editor because the file changed and we can't set it in the textfield retroactively
                    Repaint();
                }
            }

            return path;
        }

        public void Pixelate()
        {
            if (inputTexture)
            {
                inputTexture.filterMode = FilterMode.Point;
                inputTexture.wrapMode = TextureWrapMode.Repeat;
            }
        }

        public void Reduce()
        {
            if (inputTexture)
            {
                Texture2D texture = new Texture2D(inputTexture.width, inputTexture.height, TextureFormat.ARGB32 , false);
                texture = Resize(inputTexture, targetWidth, targetHeight);
                SaveToPath(texture);
                
            }

        }

        protected Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
        }

        public void SaveToPath(Texture2D texture)
        {
            try
            {
                //then Save To Disk as PNG
                byte[] bytes = texture.EncodeToPNG();
                var dirPath = Application.dataPath + outputPath;
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                File.WriteAllBytes(dirPath + "Image" + ".png", bytes);
                
                Debug.Log("Reduced texture!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


}
