public enum EVENT_TYPE
{
    SHOW_HUD,
    INIT_PLAYERDATA,
    DO_CLICK,
    SEND_TRIGGER,
    SHOW_TRIGGER,
    HIDE_TRIGGER,
    MOTION_START,

    GET_ITEM,
    SELECT_ITEM,

    //INIT_TRIGGER,
    //INIT_MOTION,
    INIT_OBJECT,
    SAVE_OBJECT
};
public enum TRIGGER_STEP
{
    FIRST,SECOND,THIRD,FOURTH
};
public enum MOTION_TYPE
{
    ROTATE,
    FADEOUT,
    FADEIN,
    MOVE,
    SHOWVIEW
}
public enum PLAY_TYPE
{
    NUMBERLOCK_NUMBER, //일반적인 비밀번호 맞추기 형태
    NUMBERLOCK_SHPAER, //색깔을 입력한다던지 하는 형태
    NUMBERLOCK_CLICKER, //버튼 순서대로 누르기 형태
}