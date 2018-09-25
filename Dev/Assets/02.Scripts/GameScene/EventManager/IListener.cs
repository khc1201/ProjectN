using UnityEngine;

/*
 * 기능 : 이벤트 발생을 위한 인터페이스 구현으로, 새로운 이벤트 타입이 있으면 이곳에 가장 먼저 등록
 */


/*
 * 목적 : 리스너 클래스에서 구현 될 리스너 인터페이스 : 이 함수를 반드시 구현해라!
 *  1. event_type : 이 이벤트 타입이 발생 되었다면
 *  2. Sender : 이 이벤트를 발생시킨 녀석
 *  3. Param : 이벤트를 발생시키며 함께 전달 할 프로퍼티
 */

public interface IListener
{
    void OnEvent(EVENT_TYPE event_type, Component Sender, object Param = null);
}