using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCL2LanguageLauncher
{
    internal static class Program
    {
        private const string RequiredLanguage = "zh-CN";

        private const string Pcl2FileName =
            "Plain Craft Launcher 2.exe";

        private static readonly string ConfigDirectory =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "PCL2LanguageLauncher");

        private static readonly string ConfigFile =
            Path.Combine(
                ConfigDirectory,
                "pcl2-path.txt");

        private static string _pcl2Path;
        private static string _originalOverride;
        private static bool _overrideChanged;

        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "PCL2 Language Launcher";

            Console.WriteLine("==============================================");
            Console.WriteLine("          PCL2 Language Launcher");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            try
            {
                // --------------------------------------------------
                // Step 1: Find PCL2
                // --------------------------------------------------

                Console.WriteLine(
                    "[1/4] Locating PCL2...");

                _pcl2Path =
                    FindPcl2();

                if (string.IsNullOrWhiteSpace(_pcl2Path))
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "PCL2 was not selected.");

                    return;
                }

                Console.WriteLine(
                    "      PCL2: " +
                    _pcl2Path);

                Console.WriteLine();

                RegisterExitHandlers();

                // --------------------------------------------------
                // Step 2: Read current Windows UI language override
                // --------------------------------------------------

                Console.WriteLine(
                    "[2/4] Reading current Windows UI language override...");

                _originalOverride =
                    GetUILanguageOverride();

                Console.WriteLine(
                    "      Original override: " +
                    (string.IsNullOrWhiteSpace(_originalOverride)
                        ? "(none)"
                        : _originalOverride));

                Console.WriteLine();

                // --------------------------------------------------
                // Step 3: Set zh-CN and start PCL2
                // --------------------------------------------------

                Console.WriteLine(
                    "[3/4] Setting Windows UI language override to zh-CN...");

                SetUILanguageOverride(
                    RequiredLanguage);

                _overrideChanged = true;

                Console.WriteLine(
                    "      Windows UI language override: zh-CN");

                Console.WriteLine();

                Console.WriteLine(
                    "      Starting PCL2...");

                Process pcl2Process =
                    StartPcl2();

                if (pcl2Process == null)
                {
                    throw new Exception(
                        "Failed to start PCL2.");
                }

                Console.WriteLine(
                    "      PCL2 started. PID = " +
                    pcl2Process.Id);

                Console.WriteLine();
                Console.WriteLine(
                    "PCL2 is running.");
                Console.WriteLine(
                    "Close PCL2 when you are finished.");
                Console.WriteLine();

                // --------------------------------------------------
                // Wait for PCL2
                // --------------------------------------------------

                pcl2Process.WaitForExit();

                Console.WriteLine();
                Console.WriteLine(
                    "PCL2 exited. Exit code = " +
                    pcl2Process.ExitCode);

                // --------------------------------------------------
                // Step 4: Restore original override
                // --------------------------------------------------

                Console.WriteLine();
                Console.WriteLine(
                    "[4/4] Restoring original Windows UI language override...");

                RestoreUILanguageOverride();

                _overrideChanged = false;

                Console.WriteLine(
                    "      Windows UI language override restored.");

                Console.WriteLine();
                Console.WriteLine(
                    "Launcher completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Launcher error:");

                Console.WriteLine(
                    ex.Message);
            }
            finally
            {
                // Make sure the override is restored even when
                // an exception occurs after changing it.
                if (_overrideChanged)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine(
                            "Cleaning up Windows UI language override...");

                        RestoreUILanguageOverride();

                        _overrideChanged = false;

                        Console.WriteLine(
                            "Windows UI language override restored.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine(
                            "WARNING: Failed to restore Windows UI language override.");

                        Console.WriteLine(
                            ex.Message);
                    }
                }
            }

            Console.WriteLine();
            Pause();
        }

        // ==========================================================
        // PCL2 Path Discovery
        // ==========================================================

        private static string FindPcl2()
        {
            // 1. Previously saved path.
            string savedPath =
                LoadSavedPcl2Path();

            if (IsValidPcl2Path(savedPath))
            {
                Console.WriteLine(
                    "      Found saved PCL2 path.");

                return savedPath;
            }

            // 2. Search relative to the launcher.
            string launcherDirectory =
                AppDomain.CurrentDomain.BaseDirectory;

            string[] relativeCandidates =
            {
                Path.Combine(
                    launcherDirectory,
                    Pcl2FileName),

                Path.Combine(
                    launcherDirectory,
                    "PCL2",
                    Pcl2FileName),

                Path.Combine(
                    Directory.GetParent(
                        launcherDirectory.TrimEnd(
                            Path.DirectorySeparatorChar))?.FullName ?? "",
                    Pcl2FileName),

                Path.Combine(
                    Directory.GetParent(
                        launcherDirectory.TrimEnd(
                            Path.DirectorySeparatorChar))?.FullName ?? "",
                    "PCL2",
                    Pcl2FileName)
            };

            foreach (string path in relativeCandidates)
            {
                if (IsValidPcl2Path(path))
                {
                    Console.WriteLine(
                        "      Found PCL2 near the launcher.");

                    SavePcl2Path(path);

                    return path;
                }
            }

            // 3. Search common locations.
            string[] commonDirectories =
            {
                @"D:\Applications\PCL2",
                @"C:\Applications\PCL2",
                @"D:\PCL2",
                @"C:\PCL2",
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.ProgramFiles),
                    "PCL2"),
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.ProgramFilesX86),
                    "PCL2")
            };

            foreach (string directory in commonDirectories)
            {
                string path =
                    Path.Combine(
                        directory,
                        Pcl2FileName);

                if (IsValidPcl2Path(path))
                {
                    Console.WriteLine(
                        "      Found PCL2 in a common location.");

                    SavePcl2Path(path);

                    return path;
                }
            }

            // 4. Ask the user to select PCL2 manually.
            Console.WriteLine();
            Console.WriteLine(
                "      PCL2 was not found automatically.");

            Console.WriteLine(
                "      Please select " +
                "\"" + Pcl2FileName + "\".");

            Console.WriteLine();

            string selectedPath =
                SelectPcl2Executable();

            if (!string.IsNullOrWhiteSpace(
                    selectedPath))
            {
                SavePcl2Path(
                    selectedPath);

                return selectedPath;
            }

            return null;
        }

        private static bool IsValidPcl2Path(
            string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            if (!File.Exists(path))
            {
                return false;
            }

            return string.Equals(
                Path.GetFileName(path),
                Pcl2FileName,
                StringComparison.OrdinalIgnoreCase);
        }

        private static string SelectPcl2Executable()
        {
            using (OpenFileDialog dialog =
                   new OpenFileDialog())
            {
                dialog.Title =
                    "Select Plain Craft Launcher 2.exe";

                dialog.Filter =
                    "Plain Craft Launcher 2 (*.exe)|*.exe";

                dialog.FileName =
                    Pcl2FileName;

                dialog.CheckFileExists =
                    true;

                dialog.Multiselect =
                    false;

                DialogResult result =
                    dialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return null;
                }

                if (!IsValidPcl2Path(
                        dialog.FileName))
                {
                    MessageBox.Show(
                        "Please select \"Plain Craft Launcher 2.exe\".",
                        "Invalid PCL2 File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return null;
                }

                return dialog.FileName;
            }
        }

        private static string LoadSavedPcl2Path()
        {
            try
            {
                if (!File.Exists(ConfigFile))
                {
                    return null;
                }

                string path =
                    File.ReadAllText(
                        ConfigFile,
                        Encoding.UTF8).Trim();

                return path;
            }
            catch
            {
                return null;
            }
        }

        private static void SavePcl2Path(
            string path)
        {
            try
            {
                Directory.CreateDirectory(
                    ConfigDirectory);

                File.WriteAllText(
                    ConfigFile,
                    path,
                    Encoding.UTF8);
            }
            catch
            {
                // Saving the path is optional.
                // The launcher can still work without it.
            }
        }

        // ==========================================================
        // PCL2 Process
        // ==========================================================

        private static Process StartPcl2()
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo
                {
                    FileName =
                        _pcl2Path,

                    WorkingDirectory =
                        Path.GetDirectoryName(
                            _pcl2Path),

                    UseShellExecute =
                        true
                };

            return Process.Start(
                startInfo);
        }

        // ==========================================================
        // Windows UI Language Override
        // ==========================================================

        private static string GetUILanguageOverride()
        {
            string output =
                RunPowerShell(
                    "$x = Get-WinUILanguageOverride; " +
                    "if ($null -eq $x) { '' } " +
                    "else { $x.Name }");

            output =
                output.Trim();

            if (string.IsNullOrWhiteSpace(
                    output))
            {
                return null;
            }

            return output;
        }

        private static void SetUILanguageOverride(
            string language)
        {
            RunPowerShell(
                "Set-WinUILanguageOverride " +
                "-Language " +
                language);
        }

        private static void RestoreUILanguageOverride()
        {
            if (string.IsNullOrWhiteSpace(
                    _originalOverride))
            {
                // No original override existed.
                // Calling Set-WinUILanguageOverride without
                // -Language clears the override.
                RunPowerShell(
                    "Set-WinUILanguageOverride");
            }
            else
            {
                // Restore the original override.
                RunPowerShell(
                    "Set-WinUILanguageOverride " +
                    "-Language " +
                    _originalOverride);
            }
        }

        // ==========================================================
        // PowerShell
        // ==========================================================

        private static string RunPowerShell(
            string command)
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo
                {
                    FileName =
                        "powershell.exe",

                    Arguments =
                        "-NoProfile " +
                        "-NonInteractive " +
                        "-ExecutionPolicy Bypass " +
                        "-Command \"" +
                        command.Replace(
                            "\"",
                            "\\\"") +
                        "\"",

                    UseShellExecute =
                        false,

                    CreateNoWindow =
                        true,

                    RedirectStandardOutput =
                        true,

                    RedirectStandardError =
                        true,

                    StandardOutputEncoding =
                        Encoding.UTF8,

                    StandardErrorEncoding =
                        Encoding.UTF8
                };

            using (Process process =
                   Process.Start(startInfo))
            {
                string output =
                    process.StandardOutput.ReadToEnd();

                string error =
                    process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception(
                        "PowerShell command failed.\r\n" +
                        error.Trim());
                }

                return output;
            }
        }

        // ==========================================================
        // Process Exit Cleanup
        // ==========================================================

        private static void RegisterExitHandlers()
        {
            Console.CancelKeyPress += delegate
            {
                try
                {
                    if (_overrideChanged)
                    {
                        RestoreUILanguageOverride();

                        _overrideChanged =
                            false;
                    }
                }
                catch
                {
                    // Ignore cleanup errors during process termination.
                }
            };

            AppDomain.CurrentDomain.ProcessExit += delegate
            {
                try
                {
                    if (_overrideChanged)
                    {
                        RestoreUILanguageOverride();

                        _overrideChanged =
                            false;
                    }
                }
                catch
                {
                    // Ignore cleanup errors during process termination.
                }
            };
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Press any key to exit...");

            Console.ReadKey(true);
        }
    }
}
