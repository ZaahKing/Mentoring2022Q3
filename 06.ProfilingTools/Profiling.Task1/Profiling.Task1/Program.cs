﻿using System.Diagnostics;
using Profiling.Task1;

byte[] salt = new byte[] { 56, 78, 66, 45, 128, 40, 67, 23, 54, 0, 0, 0, 0, 0, 0, 1 };
string[] passwords = new string[]
{
    "1234567!Alex",
    "QwertyUiopasdfghJKl;'zxcb,klkjsdhfdgajdha;sdkjkjsdlfja;zskdljza;sdh;",
    "123",
    "*&^(*&^%&^#$&(*&%^$%&^%$)",
    "Password12",
    "12345678901234567890-12375677777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777",
    "123123123123123123123123123123123123123123123123123123123123123123123123123123123",
};

for (int i = 0; i < passwords.Length; i += 1)
{
    Console.WriteLine($"For password - '{passwords[i]}'");
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var hash = Encoding.GeneratePasswordHashUsingSalt(passwords[i], salt);
    stopwatch.Stop();
    Console.WriteLine($"\tSource algorithms: {stopwatch.ElapsedTicks} ticks");
    stopwatch.Restart();
    var hash2 = Encoding.GeneratePasswordHashUsingSalt2(passwords[i], salt);
    stopwatch.Stop();
    Console.WriteLine($"\tChanged algorithms: {stopwatch.ElapsedTicks} ticks");
}

