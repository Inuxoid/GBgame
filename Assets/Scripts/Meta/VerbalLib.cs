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
        FirstCrouch,
        FirstVHS,
        FirstPuddle,
        FirstTurret,
        Elevator
    };

    public static char[] FirstCrouch = "Протискиваться в глубь вонючей ямы,\nИменумой \"городской канализацией\"...\nНе самая приятная затея".ToCharArray();
    public static char[] FirstVHS = "Классная кассета. И пусть катятся к черту те, кто сказал что Он умер в конце фильма".ToCharArray();
    public static char[] FirstPuddle = "Ну и запашок...\nПравда, вода в городе не сильно лучше.".ToCharArray();
    public static char[] FirstTurret = "Для защиты населения, говорили они.\nДа я прямо Ахиллес.\nПравда все тело – пята.".ToCharArray();
    public static char[] Elevator = "На удивление, я не испытал радости освобождения.\nЯ все еще был заперт в темнице.\nВ тюрьме иного толка...".ToCharArray();
}