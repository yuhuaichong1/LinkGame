using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedDotModule;

public static class FacadeRedDot
{
    public static Action<RDNode, int> AddRDNode_ByNode;                                                         //����ĳ����ֵ
    public static Action<string, int> AddRDNode_ByName;                                                         //����ĳ����ֵ
    public static Action<RDNode, int> ReduceRDNode_ByNode;                                                      //����ĳ����ֵ
    public static Action<string, int> ReduceRDNode_ByName;                                                      //����ĳ����ֵ
    public static Action<RDNode, Action<OverKind, int>, SetRDNodeKind> SetRDNodeAction_ByNode;                  //�ı�ĳ���Ļص�
    public static Action<string, Action<OverKind, int>, SetRDNodeKind> SetRDNodeAction_ByName;                  //�ı�ĳ���Ļص�
    public static Action<string, int, SetRDNodeKind> SetRDNodeNum_ByName;                                       //�ı�ĳ��������
    public static Action<RDNode, int, SetRDNodeKind> SetRDNodeNum_ByNode;                                       //�ı�ĳ��������
    public static Func<string, RDNode> GetRDNode;                                                               //��ȡĳ���
    public static Func<RDNode, OverKind> CheckRDNodeStatus_ByNode;                                              //�ж�Ŀ����״̬
    public static Func<string, OverKind> CheckRDNodeStatus_ByName;                                              //�ж�Ŀ����״̬
    public static Func<string, string, Action<OverKind, int>, int, int, bool, int ,bool, RDNode> CreateRDNode;  // �½�һ����㣨��maxValue == minValueʱ�����Զ�תΪbool���͵ĺ�㡾����AddRDNodeʱĬ�������numΪ1������ReduceRDNodeʱĬ�����磬numΪ0����
    public static Action<string, RDNode> CreateRDNode_Simple;                                                   //�½�����ӣ�һ�����
    public static Action<RDNode> DeleteRDNode_ByNode;                                                           //ɾ��ĳ�����
    public static Action<string> DeleteRDNode_ByName;                                                           //ɾ��ĳ�����
    public static Action RefushAllRDNode;                                                                       //ˢ�����еĺ��
    public static Action<RDNode, bool> RefushRDNode_ByNode;                                                     //ˢ��ָ�����
    public static Action<string, bool> RefushRDNode_ByName;                                                     //ˢ��ָ�����
}
