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
        FirstVHS,
        FirstPuddle,
        FirstTurret,
        Elevator,
        Elevator1,
        Elevator2,
        Door,
        Police1,
        Police2,
        Turret1,
        Turret2,
        StartDoor,
        Police
    };

    public static char[] FirstVHS = "Да не умер он в конце фильма".ToCharArray();
    public static char[] FirstPuddle = "Ну и запашок...\nПравда, вода в городе не сильно лучше".ToCharArray();
    public static char[] FirstTurret = "Для защиты населения, говорили они".ToCharArray();
    public static char[] Elevator = "На удивление, я не испытал радости освобождения".ToCharArray();
    public static char[] Elevator1 = "Я все еще был заперт в темнице".ToCharArray();
    public static char[] Elevator2 = "В тюрьме иного толка...".ToCharArray();
    public static char[] Door = "Не поддаётся".ToCharArray();
    public static char[] Police1 = "И с ними я служил бок о бок...".ToCharArray();
    public static char[] Police2 = "Здешним \"блюстителям закона\" принципы не ведомы...".ToCharArray();
    public static char[] Turret1 = "Уязвимая точка – сзади".ToCharArray();
    public static char[] Turret2 = "Неужели они думали, что меня остановит это ведро с гвоздями".ToCharArray();
    public static char[] StartDoor = "Они отрезали мне путь...\nСбежал из одной клетки, чтобы быть запертым в другой...\nнадо же...".ToCharArray();
    public static char[] Police = "Ты что-нибудь слышал?".ToCharArray();
}