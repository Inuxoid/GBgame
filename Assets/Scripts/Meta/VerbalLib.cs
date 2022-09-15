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
        Lvl2_FinalLift4,
        Lvl2_LiftControl,
        Lvl2_WTF
    };

    public static char[] FirstVHS = "Да не умер он в конце фильма".ToCharArray();
    public static char[] FirstPuddle = "Ну и запашок...\nПравда, вода в городе не сильно лучше".ToCharArray();
    public static char[] FirstTurret = "Для защиты населения, говорили они".ToCharArray();
    public static char[] Elevator = "На удивление, я не испытал радости освобождения".ToCharArray();
    public static char[] Elevator1 = "Я все еще был заперт в темнице".ToCharArray();
    public static char[] Elevator2 = "В тюрьме иного толка...".ToCharArray();
    public static char[] Door = "Не поддаётся".ToCharArray();
    public static char[] Police1 = "Внизу кто-то есть".ToCharArray();
    public static char[] Police2 = "Здешним \"блюстителям закона\" принципы не ведомы...".ToCharArray();
    public static char[] Turret1 = "Уязвимая точка – сзади".ToCharArray();
    public static char[] Turret2 = "Неужели они думали, что это меня остановит".ToCharArray();
    public static char[] StartDoor = "Они отрезали мне путь...\nСбежал из одной клетки, чтобы быть запертым в другой...\nнадо же...".ToCharArray();
    public static char[] Police = "Ты что-нибудь слышал?".ToCharArray();
    public static char[] Lvl2_Back = "Я не могу пойти обратно".ToCharArray();
    public static char[] Lvl2_Finish = "Соседняя башня - моя цель. \nИменно там мы и встретимся, чудовище.       \nА это еще что?       \nБосс - вертолет?!      \nОни действительно сделали это?".ToCharArray();
    public static char[] Lvl2_Phone1 = "Что-то мне подсказывает - надо взять трубку".ToCharArray();
    public static char[] Lvl2_Phone2 = "Поддержка незнакомца не повредит".ToCharArray();
    public static char[] Lvl2_Street = "Проникнуть внутрь. Потом на самый верх..".ToCharArray();
    public static char[] Lvl2_Camera_first = "Камеры.. Как я и думал".ToCharArray();
    public static char[] Lvl2_Camera_terminal = "Ключ доступа пригодился".ToCharArray();
    public static char[] Lvl2_Cyborg_replick = "Ненавижу киборгов. Самая мерзкая охранная система".ToCharArray();
    public static char[] Lvl2_ventilation = "Склад оружия, замаскированный под офисное здание – он даже не старается это скрыть".ToCharArray();
    public static char[] Lvl2_FinalLift1 = "Лилиан...".ToCharArray();
    public static char[] Lvl2_FinalLift2 = "Хотел бы я вернуть все назад.".ToCharArray();
    public static char[] Lvl2_FinalLift3 = "Не лезть на рожон, не переходить ему дорогу...".ToCharArray();
    public static char[] Lvl2_FinalLift4 = "Но ничего не вернуть. И мне с этим жить.".ToCharArray();
    public static char[] Lvl2_LiftControl = "В работе лифта что-то изменится?".ToCharArray();
    public static char[] Lvl2_WTF = "Поздравляем! Вы добились своего, тут обязательно должен был быть баг и вы его нашли. Можете перезапустить игру или подойти к порталу и начать сначала, либо живите дальше в этом проклятом мире который сами и создали".ToCharArray();

}