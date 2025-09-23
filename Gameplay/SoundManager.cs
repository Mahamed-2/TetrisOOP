using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TetrisOOP.Gameplay
{
    public static class SoundManager
    {
        private static Process? _audioProcess;
        private static bool _stopMusic = false;
        private static readonly string? _musicFilePath;

        static SoundManager()
        {
            // Get the base directory where the app is running from
            string baseDir = AppContext.BaseDirectory;
            Console.WriteLine($"[SoundManager] Base directory: {baseDir}");

            // Look for Assets folder in these locations (in order of priority)
            string[] searchPaths = {
                Path.Combine(baseDir, "Assets", "tetris.mp3"),          // 1. Next to executable (most reliable)
                Path.Combine("Assets", "tetris.mp3"),                   // 2. Relative to current directory
                Path.Combine("..", "Assets", "tetris.mp3"),             // 3. One level up
                Path.Combine("..", "..", "..", "Assets", "tetris.mp3"), // 4. Project root from bin/Debug/net9.0/
                "tetris.mp3"                                            // 5. Directly in current directory
            };

            foreach (var path in searchPaths)
            {
                string fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath))
                {
                    _musicFilePath = fullPath;
                    Console.WriteLine($"[SoundManager] Found music file: {_musicFilePath}");
                    return;
                }
                Console.WriteLine($"[SoundManager] Checked: {fullPath}");
            }

            Console.WriteLine("[SoundManager] Warning: Music file not found in any location");
            Console.WriteLine("[SoundManager] Using visual fallback mode");
            _musicFilePath = null;
        }

        public static void PlayTetrisTheme()
        {
            if (string.IsNullOrEmpty(_musicFilePath))
            {
                PlayVisualMelody();
                return;
            }

            _stopMusic = false;

            new Thread(() =>
            {
                try
                {
                    // macOS: Use afplay in a loop
                    while (!_stopMusic)
                    {
                        _audioProcess = new Process();
                        _audioProcess.StartInfo.FileName = "/usr/bin/afplay";
                        _audioProcess.StartInfo.Arguments = $"\"{_musicFilePath}\"";
                        _audioProcess.StartInfo.UseShellExecute = false;
                        _audioProcess.StartInfo.CreateNoWindow = true;
                        _audioProcess.Start();
                        _audioProcess.WaitForExit(); // Wait for song to finish

                        if (_stopMusic) break;
                        Thread.Sleep(50); // Short pause before restarting
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SoundManager Error] {ex.Message}");
                    PlayVisualMelody();
                }
            }).Start();
        }

        private static void PlayVisualMelody()
        {
            string[] melody = { "E", "B", "C", "D", "C", "B", "A" };
            int[] durations = { 300, 150, 150, 300, 150, 150, 300 };

            while (!_stopMusic)
            {
                for (int i = 0; i < melody.Length; i++)
                {
                    if (_stopMusic) break;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"♪ {melody[i]}");
                    Console.ResetColor();
                    Thread.Sleep(durations[i]);
                }
            }
        }

        public static void StopMusic()
        {
            _stopMusic = true;
            try
            {
                if (_audioProcess != null && !_audioProcess.HasExited)
                {
                    _audioProcess.Kill();
                    _audioProcess.Dispose();
                }
            }
            catch
            {
                // Ignore errors during cleanup
            }
            finally
            {
                _audioProcess = null;
            }
        }
    }
}