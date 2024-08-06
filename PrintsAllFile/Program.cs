using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // 시작 폴더 경로를 지정합니다.
        string startPath = @"C:\Users\c\Downloads";

        // 결과를 저장할 파일 경로를 지정합니다.
        string outputPath = @"C:\Users\c\Downloads\output.txt";

        // 폴더 내 모든 파일과 폴더를 출력합니다.
        PrintAllFiles(startPath, outputPath);
    }

    static void PrintAllFiles(string startPath, string outputPath)
    {
        // 탐색할 폴더 경로를 저장하는 큐를 생성합니다.
        Queue<string> folders = new Queue<string>();
        folders.Enqueue(startPath);
        StreamWriter writer = new StreamWriter(outputPath);

        while (folders.Count > 0)
        {
            // 큐에서 현재 폴더를 가져옵니다.
            string currentFolder = folders.Dequeue();

            try
            {
                // 현재 폴더 내의 모든 파일을 출력합니다.
                foreach (string file in Directory.GetFiles(currentFolder))
                {
                    Console.WriteLine(file);
                    FileInfo fileInfo = new FileInfo(file);
                    string fileInfoText = $"{fileInfo.FullName},{fileInfo.CreationTime},{fileInfo.LastWriteTime}";
                    writer.WriteLine(fileInfoText);
                }

                // 현재 폴더 내의 모든 하위 폴더를 큐에 추가합니다.
                foreach (string folder in Directory.GetDirectories(currentFolder))
                {
                    folders.Enqueue(folder);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                // 접근할 수 없는 폴더가 있을 경우 예외를 처리합니다.
                Console.WriteLine($"Access denied to folder: {currentFolder}, {e.Message}");
            }
            catch (DirectoryNotFoundException e)
            {
                // 폴더가 없을 경우 예외를 처리합니다.
                Console.WriteLine($"Directory not found: {currentFolder}, {e.Message}");
            }
        }
        writer.Close();
    }
}
