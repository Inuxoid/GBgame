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
        Police,
        Lvl2_Back,
        Lvl2_Finish,
        Lvl2_Phone1,
        Lvl2_Phone2,
        Lvl2_Street,
        Lvl2_Camera_first,
        Lvl2_Camera_terminal,
        Lvl2_Cyborg_replick,
        Lvl2_ventilation,
        Lvl2_FinalLift1,
        Lvl2_FinalLift2,
        Lvl2_FinalLift3,
        Lvl2_FinalLift4
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
    public static char[] Lvl2_Back = "Не отступать и не сдаваться".ToCharArray();
    public static char[] Lvl2_Finish = "Соседняя башня - моя цель. Именно там мы и встретимся, чудовище. Хмм... А это что еще? Я обязательно разнесу тебя на куски. Но чуть позже".ToCharArray();
    public static char[] Lvl2_Phone1 = "Телефон? Какого чёрта? Что-то мне подсказывает - надо взять трубку".ToCharArray();
    public static char[] Lvl2_Phone2 = "Стоит воспользоваться поддержкой моего неизвестного приятеля".ToCharArray();
    public static char[] Lvl2_Street = "Проникнуть внутрь. Потом на самый верх.. Дальше по обстановке".ToCharArray();
    public static char[] Lvl2_Camera_first = "Камеры. Я так и думал. Где-то должен быть терминал".ToCharArray();
    public static char[] Lvl2_Camera_terminal = "Терминал охраны. Идиоты поставили его под самым носом. Только вот на сколько он отключает камеры?".ToCharArray();
    public static char[] Lvl2_Cyborg_replick = "Ненавижу киборгов. Самая мерзкая охранная система".ToCharArray();
    public static char[] Lvl2_ventilation = "Канализация.. вентиляция. Ублюдкам меня не остановить. Нужно подняться выше".ToCharArray();
    public static char[] Lvl2_FinalLift1 = "Линайна..  Лин".ToCharArray();
    public static char[] Lvl2_FinalLift2 = "Что бы ты сказала увидев меня сейчас?".ToCharArray();
    public static char[] Lvl2_FinalLift3 = "Я стал противоположностью себя прежнего?".ToCharArray();
    public static char[] Lvl2_FinalLift4 = "Человеком, которого ты никогда бы не смогла полюбить".ToCharArray();
}