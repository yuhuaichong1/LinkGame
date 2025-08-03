using UnityEngine;
using SimpleJSON;

public class RewardData {
	public int type;//奖励类型
	public int number;//奖励数量
    public int probability0;//无星奖励获取奖励概率（x/60）
	public int probability1;//1星奖励获取奖励概率（x/60）
    public int probability2;//2星奖励获取奖励概率（x/60）
    public int probability3;//3星奖励获取奖励概率（x/60）
    public RewardData(int type, int number, int probability0, int probability1, int probability2, int probability3) {
		this.type = type;
		this.number = number;
		this.probability0 = probability0;
		this.probability1 = probability1;
		this.probability2 = probability2;
		this.probability3 = probability3;
	}

	public RewardData (JSONClass data) {
		this.type = data [LevelDefines.type].AsInt;
		this.number = data [LevelDefines.number].AsInt;
		this.probability0 = data [LevelDefines.probability + "0"].AsInt;
		this.probability1 = data [LevelDefines.probability + "1"].AsInt;
		this.probability2 = data [LevelDefines.probability + "2"].AsInt;
		this.probability3 = data [LevelDefines.probability + "3"].AsInt;
	}

    //根据通过时间（星数）判断是否获得奖励（概率）
    public int getReward(int star) {
		int reward = 0;
        int probability = Random.Range(0, 60);
        switch (star) {
		case 0:
			if (probability < probability0) {
				reward = number;
			}
			break;
		case 1:
			if (probability < probability1) {
				reward = number;
			}
			break;
		case 2:
			if (probability < probability2) {
				reward = number;
			}
			break;
		case 3:
			if (probability < probability3) {
				reward = number;
			}
			break;
		}
        return reward;
	}
}
