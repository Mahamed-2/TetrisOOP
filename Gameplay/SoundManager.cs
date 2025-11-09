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
            string executableDir = AppContext.BaseDirectory;
            string assetsPath = Path.Combine(executableDir, "Assets", "tetris.mp3");
            
            if (File.Exists(assetsPath))
            {
                _musicFilePath = assetsPath;
                Console.WriteLine($"[SoundManager] Music file found: {_musicFilePath}");
            }
            else
            {
                Console.WriteLine("[SoundManager] Music file not found. Using visual mode.");
                _musicFilePath = null;
            }
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
                    while (!_stopMusic)
                    {
                        using (var process = new Process())
                        {
                            process.StartInfo.FileName = "/usr/bin/afplay";
                            process.StartInfo.Arguments = $"\"{_musicFilePath}\"";
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.Start();
                            process.WaitForExit();
                        }
                        if (_stopMusic) break;
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Music Error] {ex.Message}");
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

        // ONLY ONE StopMusic METHOD
        public static void StopMusic()
        {
            _stopMusic = true;
            try
            {
                _audioProcess?.Kill();
                _audioProcess?.Dispose();
            }
            catch
            {
                // Ignore cleanup errors
            }
            finally
            {
                _audioProcess = null;
            }
        }
    }
}