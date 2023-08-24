using System.Collections.Generic;

class VerbalLib
{
    private static VerbalLib instance;

    private VerbalLib()
    { }

    public static VerbalLib getInstance()
    {
        if (instance == null)
            instance = new VerbalLib();
        return instance;
    }

    
    public List<char[]> texts = new()
    {
        Replic_1,
        Replic_2,
        Replic_8,
        endTitle
    };

    public static char[] Replic_1 = "*click* I'd rather be here. Among the stench, waste and rotting corpses. Just not on the surface. It's better here. Safer. *click*".ToCharArray();
    public static char[] Replic_2 = "*click* I shouldn't have come down, Elaine. Here, people in robes are sneaking around everywhere. I met one.He immediately ran away.Scared? Or did he go for help? *click*".ToCharArray();
    public static char[] Replic_8 = "*Meles Times newspaper. 12.9.2158* ...and may Hades be glorified, restraining the infidels with his grip, Apollo, who showed us the way to the beautiful, Hephaestus, who blessed our creation. *end of title*".ToCharArray();
    public static char[] endTitle = "Уровень пройден. Однако это технический билд и мы не можем пустить тебя дальше. Но ты можешь перезапустить уровень и попробовать другие варианты прохождения. Но еcли уже уходишь, спасибо тебе за принятие участия в тестировании! Будем признательны за обратную связь по билду.".ToCharArray();
}