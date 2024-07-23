using System;
using System.IO;

class Program
{
    static void Main()
    {
        // 감시할 디렉터리 경로 설정
        string directoryToWatch = @"C:\Users\c\Documents";

        // FileSystemWatcher 인스턴스 생성 및 설정
        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.InternalBufferSize = 64 * 1024; // 64KB
            watcher.Path = directoryToWatch;

            // 감시할 변경 유형 설정
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;

            // 감시할 파일 유형 설정 (모든 파일 감시)
            watcher.Filter = "*.*";

            // 이벤트 핸들러 추가
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            // 감시 시작
            watcher.EnableRaisingEvents = true;

            // 사용자 입력을 기다려 프로그램이 종료되지 않도록 함
            Console.WriteLine("Press 'q' to quit the sample.");
            while (Console.Read() != 'q') ;
        }
    }

    // 파일이 변경, 생성, 삭제될 때 호출되는 이벤트 핸들러
    private static void OnChanged(object source, FileSystemEventArgs e) =>
        Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

    // 파일 이름이 변경될 때 호출되는 이벤트 핸들러
    private static void OnRenamed(object source, RenamedEventArgs e) =>
        Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
}
