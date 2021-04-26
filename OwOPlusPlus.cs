using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OwOPlusPlus
{
    // ReSharper disable once UnusedMember.Global
    public sealed class OwOPlusPlus : Mod
    {
        public Type LanguageManagerType => typeof(LanguageManager);

        public Type LocalizedTextType => typeof(LocalizedText);

        public FieldInfo LocalizedTextsInfo =>
            LanguageManagerType.GetField("_localizedTexts", BindingFlags.Instance | BindingFlags.NonPublic);

        public ConstructorInfo LocalizedTextBuilder =>
            LocalizedTextType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First();

        string[] CursedText => new[] {
            " uwu",
            " UwU",
            " owo",
            " OwO",
            " >w<",
            " >//w//<",
            " .w.",
            " _w_",
            " QwQ",
            " qwq",
            " rawr",
            " *nuzzles*",
            " x3",
            " ;3",
            " :3",
            " purrrr",
            " *notices* what's this?",
            "...",
            "~~",
            "~",
            "!~",
            "!!",
            ",,,",
            ",",
            " *cries*"
        };

        public override void PostSetupContent()
        {
            if (LocalizedTextsInfo.GetValue(LanguageManager.Instance) is not Dictionary<string, LocalizedText> texts)
                return;

            LocalizedText[] textArray = texts.Values.ToArray();
            LocalizedText[] valueArray = texts.Values.ToArray();
            string[] keyArray = texts.Keys.ToArray();

            for (int i = 0; i < texts.Count; i++)
            {
                string text = textArray[i].Value;
                text = text.Replace('r', 'w')
                    .Replace('R', 'W')
                    .Replace("ove", "uv")
                    .Replace("OVE", "UV")
                    .Replace("oo", "owo")
                    .Replace("OO", "OwO")
                    .Replace("u", "uwu")
                    .Replace("uu", "uwu")
                    .Replace("U", "UwU")
                    .Replace("UU", "UwU")
                    .Replace("l", "w")
                    .Replace("L", "W")
                    .Replace("th", Main.rand.NextBool() ? "d" : "ff")
                    .Replace("Th", Main.rand.NextBool() ? "D" : "FF")
                    .Replace("TH", Main.rand.NextBool() ? "D" : "FF");

                text += CursedText[Main.rand.Next(CursedText.Length)];

                valueArray[i] = LocalizedTextBuilder.Invoke(valueArray[i], new object?[2] { keyArray[i], text }) as LocalizedText;
            }

            LocalizedTextsInfo.SetValue(LanguageManager.Instance, texts);
        }
    }
}