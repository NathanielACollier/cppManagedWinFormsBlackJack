using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace nac.CardImage;

public static class Card
{


    public static List<string> GetCardList()
    {
        Assembly asm = typeof(nac.CardImage.Card).Assembly;

        var cardFiles = asm.GetManifestResourceNames()
                  .Where(name =>
                         name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                  .ToList();

        return cardFiles;
    }
}
