﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour {

    #region : SceneData 의 구성
    /*
    1. Scene 의 변동 데이터에 대한 정보를 가지고 있다가, Load 에서 진행 상태를 저장한다.
    2. Save
        3.1 TriggerUnit 가 발동되면 index 의 순서를 List 에 저장 시킨다.
        3.2 MotionUnit 이 발동되면 index 의 순서를 List 에 저장 시킨다.
    3. Load
        3.1 TriggerUnit 의 IsTriggered 정보를 로드 및 지정한다.
        3.2 MotionTrigger 의 IsTriggered 정보를 로드 및 지정한다.
            -MotionTrigger 는 IsTriggered 가 활성화 된 상태에서 신호를 받으면 Motion 의 사운드 및 시간을 0f 로 지정하고 수행한다.
        3.3	TriggerUnit 을 순차적으로 발동시킨다. 
    */
    #endregion

    void Start () {
		//TriggerListener 의 IsTriggered 를 대조하여 PlayerData 정보에 맞게 저장한다.
        //
        //TriggerListener 에게 IsTriggered 체크 된 TriggerUnit 을 실행하도록 한다.
	}
}
