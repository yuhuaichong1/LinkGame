using UnityEngine;
using System.Collections;

public class AutoGenData {

	public int type;
	public float timeWait;
	public float timeRun;

	public AutoGenData (int type, float timeWait, float timeRun) {
		this.type = type;//???都是1，不知是干啥的，可能是用于冰冻组分类的
		this.timeWait = timeWait;//时间间隔
		this.timeRun = timeRun;//冰冻延迟
	}
}
