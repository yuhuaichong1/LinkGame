using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedDotModule;

public static class FacadeRedDot
{
    public static Action<RDNode, int> AddRDNode_ByNode;                                                         //增加某红点的值
    public static Action<string, int> AddRDNode_ByName;                                                         //增加某红点的值
    public static Action<RDNode, int> ReduceRDNode_ByNode;                                                      //减少某红点的值
    public static Action<string, int> ReduceRDNode_ByName;                                                      //减少某红点的值
    public static Action<RDNode, Action<OverKind, int>, SetRDNodeKind> SetRDNodeAction_ByNode;                  //改变某红点的回调
    public static Action<string, Action<OverKind, int>, SetRDNodeKind> SetRDNodeAction_ByName;                  //改变某红点的回调
    public static Action<string, int, SetRDNodeKind> SetRDNodeNum_ByName;                                       //改变某红点的数量
    public static Action<RDNode, int, SetRDNodeKind> SetRDNodeNum_ByNode;                                       //改变某红点的数量
    public static Func<string, RDNode> GetRDNode;                                                               //获取某红点
    public static Func<RDNode, OverKind> CheckRDNodeStatus_ByNode;                                              //判断目标红点状态
    public static Func<string, OverKind> CheckRDNodeStatus_ByName;                                              //判断目标红点状态
    public static Func<string, string, Action<OverKind, int>, int, int, bool, int ,bool, RDNode> CreateRDNode;  // 新建一个红点（当maxValue == minValue时，会自动转为bool类型的红点【调用AddRDNode时默认溢出，num为1，调用ReduceRDNode时默认下溢，num为0】）
    public static Action<string, RDNode> CreateRDNode_Simple;                                                   //新建（添加）一个红点
    public static Action<RDNode> DeleteRDNode_ByNode;                                                           //删除某个红点
    public static Action<string> DeleteRDNode_ByName;                                                           //删除某个红点
    public static Action RefushAllRDNode;                                                                       //刷新所有的红点
    public static Action<RDNode, bool> RefushRDNode_ByNode;                                                     //刷新指定红点
    public static Action<string, bool> RefushRDNode_ByName;                                                     //刷新指定红点
}
