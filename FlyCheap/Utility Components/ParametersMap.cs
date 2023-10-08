using FlyCheap.Enums;
using System.Collections.Generic;
using System.Collections.Generic;

namespace FlyCheap.Utility_Components;
public static class ParametersMap
{
    public static Dictionary<TableCode, string> TableFileMappings { get; } = new()
    {
        { TableCode.Airports, "airports.json" },
        { TableCode.Cities, "cities.json" },
        { TableCode.Airlines, "airlines.json" },
        { TableCode.Countries, "countries.json" },
        { TableCode.Planes, "planes.json" },
        { TableCode.Allianses, "alliances.json" },
        { TableCode.Routes, "routes.json" },
    };   
    
    public static Dictionary<LanguageCode, string> LanguageMappings { get; } = new()
    {
        {LanguageCode.None, ""},
        { LanguageCode.Arabic, "ar/" },
        { LanguageCode.Azerbaijani, "az/" },
        { LanguageCode.Czech, "cs/" },
        { LanguageCode.Danish, "da/" },
        { LanguageCode.German, "de/" },
        { LanguageCode.Greek, "el/" },
        { LanguageCode.EnglishUK, "en/" },
        { LanguageCode.EnglishAustralia, "en-AU/" },
        { LanguageCode.EnglishCanada, "en-CA/" },
        { LanguageCode.EnglishGreatBritain, "en-GB/" },
        { LanguageCode.EnglishIreland, "en-IE/" },
        { LanguageCode.EnglishIndia, "en-IN/" },
        { LanguageCode.EnglishNewZealand, "en-NZ/" },
        { LanguageCode.EnglishSingapore, "en-SG/" },
        { LanguageCode.Spanish, "es/" },
        { LanguageCode.Persian, "fa/" },
        { LanguageCode.Finnish, "fi/" },
        { LanguageCode.French, "fr/" },
        { LanguageCode.Hebrew, "he/" },
        { LanguageCode.Hindi, "hi/" },
        { LanguageCode.Croatian, "hr/" },
        { LanguageCode.Hungarian, "hu/" },
        { LanguageCode.Armenian, "hy/" },
        { LanguageCode.Indonesian, "id/" },
        { LanguageCode.Italian, "it/" },
        { LanguageCode.Japanese, "jp/" },
        { LanguageCode.Georgian, "ka/" },
        { LanguageCode.Korean, "ko/" },
        { LanguageCode.Lithuanian, "lt/" },
        { LanguageCode.Latvian, "lv/" },
        { LanguageCode.Malay, "ms/" },
        { LanguageCode.Dutch, "nl/" },
        { LanguageCode.Norwegian, "no/" },
        { LanguageCode.Polish, "pl/" },
        { LanguageCode.Portuguese, "pt/" },
        { LanguageCode.BrazilianPortuguese, "pt_br/" },
        { LanguageCode.Romanian, "ro/" },
        { LanguageCode.Russian, "ru/" },
        { LanguageCode.Slovak, "sk/" },
        { LanguageCode.Slovenian, "sl/" },
        { LanguageCode.Serbian, "sr/" },
        { LanguageCode.Swedish, "sv/" },
        { LanguageCode.Thai, "th/" },
        { LanguageCode.Tagalog, "tl/" },
        { LanguageCode.Turkish, "tr/" },
        { LanguageCode.Ukrainian, "uk/" },
        { LanguageCode.Vietnamese, "vi/" },
        { LanguageCode.ChineseSimplifiedCharacters, "zh-hans/" },
        { LanguageCode.ChineseTraditionalHieroglyphs, "zh-hant/" }
    };
}