using UnityEngine;
using System.Collections;

public class ConstraintData {
	public int direction;
	public Vec2 cell1;
	public Vec2 cell2;
	public ConstraintData(int direction, Vec2 cell1, Vec2 cell2) {
		this.direction = direction;//方向
		this.cell1 = new Vec2(cell1);//移动区域的左上角
		this.cell2 = new Vec2(cell2);//移动区域的右下角
	}
}
