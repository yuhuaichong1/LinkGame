using cfg;
using System;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

public class RedDotModule : BaseModule
{
    public class RDNode//�����ࣺ���ڵ�
    {
        public int num;//��ǰ�ڵ��µĺ������
        public RDNode Father;//��������
        public Action<OverKind, int> changeAction;//�ı�����ʱ�Ļص�����

        public int maxValue;//�ɽ������ֵ
        public int minValue;//�ɽ�����Сֵ
        public bool autoCorrectMax;//��ֵ����ʱ�Զ�����
        public bool autoCorrectMin;//��ֵ��Сʱ�Զ�����
    }

    private Dictionary<string, RDNode> RDNodeDic;//�������Ĵʵ䣬������

    /// <summary>
    /// Module��ʼ��
    /// </summary>
    protected override void OnLoad()
    {
        RDNodeDic = new Dictionary<string, RDNode>();

        FacadeRedDot.AddRDNode_ByNode += AddRDNode;
        FacadeRedDot.AddRDNode_ByName += AddRDNode;
        FacadeRedDot.ReduceRDNode_ByNode += ReduceRDNode;
        FacadeRedDot.ReduceRDNode_ByName += ReduceRDNode;
        FacadeRedDot.SetRDNodeAction_ByNode += SetRDNodeAction;
        FacadeRedDot.SetRDNodeAction_ByName += SetRDNodeAction;
        FacadeRedDot.SetRDNodeNum_ByNode += SetRDNodeNum;
        FacadeRedDot.SetRDNodeNum_ByName += SetRDNodeNum;
        FacadeRedDot.GetRDNode += GetRDNode;
        FacadeRedDot.CheckRDNodeStatus_ByNode += CheckRDNodeStatus;
        FacadeRedDot.CheckRDNodeStatus_ByName += CheckRDNodeStatus;
        FacadeRedDot.CreateRDNode += CreateRDNode;
        FacadeRedDot.CreateRDNode_Simple += CreateRDNode;
        FacadeRedDot.DeleteRDNode_ByNode += DeleteRDNode;
        FacadeRedDot.DeleteRDNode_ByNode += DeleteRDNode;
        FacadeRedDot.RefushAllRDNode += RefushRDNode;
        FacadeRedDot.RefushRDNode_ByNode += RefushRDNode;
        FacadeRedDot.RefushRDNode_ByName += RefushRDNode;

        Dictionary<int, ConfReddot> reddots = ConfigModule.Instance.Tables.TBReddot.DataMap;
        foreach (ConfReddot item in reddots.Values)
        {
            string keyName = item.KeyName;
            int father = item.Father;
            string ui = item.Path;
            CreateRDNode(keyName, item.Father == 0 ? null : reddots[father].KeyName);
        }

        //���ܻ����������õĸ�ֵ��ʽ��
        GameDefines.Reddot_Name_Out = reddots[10001].KeyName;
        GameDefines.Reddot_Name_Daily = reddots[10002].KeyName;
        GameDefines.Reddot_Name_Challenge = reddots[10003].KeyName;
        GameDefines.Reddot_Name_DailyOutBy = reddots[10004].KeyName;
        GameDefines.Reddot_Name_DailyBy = reddots[10005].KeyName;
        GameDefines.Reddot_Name_ChallengeOutBy = reddots[10006].KeyName;
        GameDefines.Reddot_Name_ChallengeBy = reddots[10007].KeyName;
    }

    //====================================================================================================================================//

    /// <summary>
    /// ����ĳ����ֵ
    /// </summary>
    /// <param name="rdNode">Ŀ����</param>
    /// <param name="value">���ӵ�ֵ</param>
    public void AddRDNode(RDNode rdNode, int value = 1)
    {
        if (!RDNodeDic.ContainsValue(rdNode))
        {
            Debug.LogError($"The rdNode is not exist.");
            return;
        }

        RDNode currentNode = rdNode;
        while (true)
        {
            OverKind overkind;
            if (currentNode.minValue == currentNode.maxValue)
            {
                bool b = currentNode.num != 1;
                currentNode.num = 1;
                currentNode.changeAction?.Invoke(OverKind.Overflow, 1);

                if (b)
                {
                    if (currentNode.Father == null)
                        return;
                    else
                        currentNode = currentNode.Father;
                }
                else
                {
                    return;
                }
            }
            else
            {
                currentNode.num += value;

                overkind = CheckRDNodeStatus(currentNode);
                if (overkind == OverKind.Overflow && currentNode.autoCorrectMax)
                    currentNode.num = currentNode.maxValue;

                currentNode.changeAction?.Invoke(overkind, currentNode.num);
                if (currentNode.Father == null)
                    return;
                else
                    currentNode = currentNode.Father;
            }
        }
    }

    /// <summary>
    /// ����ĳ����ֵ
    /// </summary>
    /// <param name="name">Ŀ��������</param>
    /// <param name="value">���ӵ�ֵ</param>
    public void AddRDNode(string name, int value = 1)
    {
        if (!RDNodeDic.ContainsKey(name))
        {
            Debug.LogError($"RedDot Node '{name}' is not exist.");
            return;
        }

        AddRDNode(RDNodeDic[name], value);
    }

    //====================================================================================================================================//

    /// <summary>
    /// ����ĳ����ֵ
    /// </summary>
    /// <param name="rdNode">Ŀ����</param>
    /// <param name="value">���ٵ�ֵ</param>
    public void ReduceRDNode(RDNode rdNode, int value = 1)
    {
        if (!RDNodeDic.ContainsValue(rdNode))
        {
            Debug.LogError($"The rdNode is not exist.");
            return;
        }

        RDNode currentNode = rdNode;
        while (true)
        {
            OverKind overkind;
            if (currentNode.minValue == currentNode.maxValue)
            {
                bool b = currentNode.num != 0;
                currentNode.num = 0;
                currentNode.changeAction?.Invoke(OverKind.Underflow, 0);

                if (b)
                {
                    if (currentNode.Father == null)
                        return;
                    else
                        currentNode = currentNode.Father;
                }
                else
                {
                    return;
                }
            }
            else
            {
                currentNode.num -= value;

                overkind = CheckRDNodeStatus(currentNode);
                if (overkind == OverKind.Underflow && currentNode.autoCorrectMin)
                    currentNode.num = currentNode.minValue;

                currentNode.changeAction?.Invoke(overkind, currentNode.num);

                if (currentNode.Father == null)
                    break;
                else
                    currentNode = currentNode.Father;
            }
        }
    }

    /// <summary>
    /// ����ĳ����ֵ
    /// </summary>
    /// <param name="name">Ŀ��������</param>
    /// <param name="value">���ٵ�ֵ</param>
    public void ReduceRDNode(string name, int value = 1)
    {
        if (!RDNodeDic.ContainsKey(name))
        {
            Debug.LogError($"RedDot Node '{name}' is not exist.");
            return;
        }

        ReduceRDNode(RDNodeDic[name], value);
    }

    //====================================================================================================================================//

    /// <summary>
    /// �ı�ĳ���Ļص�
    /// </summary>
    /// <param name="rdNode">Ŀ����</param>
    /// <param name="action">����</param>
    /// <param name="kind">�����Ĳ���</param>
    public void SetRDNodeAction(RDNode rdNode, Action<OverKind, int> action, SetRDNodeKind kind = SetRDNodeKind.Replace)
    {
        if (!RDNodeDic.ContainsValue(rdNode))
        {
            Debug.LogError($"The rdNode is not exist.");
            return;
        }
        switch (kind)
        {
            case SetRDNodeKind.Add:
                rdNode.changeAction += action;
                break;
            case SetRDNodeKind.Remove:
                rdNode.changeAction -= action;
                break;
            case SetRDNodeKind.Clear:
                rdNode.changeAction = null;
                break;
            case SetRDNodeKind.Replace:
                rdNode.changeAction = action;
                break;
        }
    }

    /// <summary>
    /// �ı�ĳ���Ļص�
    /// </summary>
    /// <param name="name">Ŀ��������</param>
    /// <param name="action">����</param>
    /// <param name="kind">�����Ĳ���</param>
    public void SetRDNodeAction(string name, Action<OverKind, int> action, SetRDNodeKind kind = SetRDNodeKind.Replace)
    {
        if (!RDNodeDic.ContainsKey(name))
        {
            Debug.LogError($"RedDot Node '{name}' is not exist.");
            return;
        }
        SetRDNodeAction(RDNodeDic[name], action, kind);
    }

    //====================================================================================================================================//
   
    /// <summary>
    /// �ı�ĳ��������
    /// </summary>
    /// <param name="rdNode">Ŀ����</param>
    /// <param name="num">����Ŀ</param>
    /// <param name="kind">����Ŀ�Ĳ���</param>
    public void SetRDNodeNum(RDNode rdNode, int num, SetRDNodeKind kind = SetRDNodeKind.Replace)
    {
        if (!RDNodeDic.ContainsValue(rdNode))
        {
            Debug.LogError($"The rdNode is not exist.");
            return;
        }

        int oldValue = rdNode.num;
        int newValue = 0;

        switch (kind)
        {
            case SetRDNodeKind.Add:
                //rdNode.num += num;
                newValue = rdNode.num + num;
                break;
            case SetRDNodeKind.Remove:
                //rdNode.num -= num;
                newValue = rdNode.num - num;
                break;
            case SetRDNodeKind.Clear:
                //rdNode.num = 0;
                newValue = 0;
                break;
            case SetRDNodeKind.Replace:
                //rdNode.num = num;
                newValue = num;
                break;
        }

        int transformation = newValue - oldValue;
        if(transformation > 0)
            AddRDNode(rdNode, transformation);
        else if(transformation < 0)
            ReduceRDNode(rdNode, -transformation);
    }

    /// <summary>
    /// �ı�ĳ��������
    /// </summary>
    /// <param name="name">Ŀ��������</param>
    /// <param name="num">����Ŀ</param>
    /// <param name="kind">����Ŀ�Ĳ���</param>
    public void SetRDNodeNum(string name, int num, SetRDNodeKind kind = SetRDNodeKind.Replace)
    {
        if(!RDNodeDic.ContainsKey(name))
        {
            Debug.LogError($"RedDot Node '{name}' is not exist.");
            return;
        }
        SetRDNodeNum(RDNodeDic[name], num, kind);
    }

    //====================================================================================================================================//

    /// <summary>
    /// ��ȡĳ���
    /// </summary>
    /// <param name="name">�������</param>
    /// <returns>Ŀ����</returns>
    public RDNode GetRDNode(string name)
    {
        if (RDNodeDic.ContainsKey(name))
            return RDNodeDic[name];
        else
        {
            Debug.LogError($"RedDot Node '{name}' is not exist.");
            return null;
        }  
    }

    //====================================================================================================================================//

    /// <summary>
    /// �ж�Ŀ����״̬
    /// </summary>
    /// <param name="rdNode">Ŀ����</param>
    /// <returns>Ŀ����״̬</returns>
    public OverKind CheckRDNodeStatus(RDNode rdNode)
    {
        int num = rdNode.num;
        if(num < rdNode.minValue) 
        { 
            return OverKind.Underflow;
        }
        else if(num > rdNode.maxValue) 
        {
            return OverKind.Overflow;
        }
        else
        {
            return OverKind.InRange;
        }
    }

    /// <summary>
    /// �ж�Ŀ����״̬
    /// </summary>
    /// <param name="name">Ŀ��������</param>
    /// <returns></returns>
    public OverKind CheckRDNodeStatus(string name)
    {
        return CheckRDNodeStatus(RDNodeDic[name]);
    }

    //====================================================================================================================================//

    /// <summary>
    /// �½�һ����㣨��maxValue == minValueʱ�����Զ�תΪbool���͵ĺ�㡾����AddRDNodeʱĬ�������numΪ1������ReduceRDNodeʱĬ�����磬numΪ0����
    /// </summary>
    /// <param name="name">�������</param>
    /// <param name="fatherName">���������</param>
    /// <param name="action">�ı�ֵʱ�Ļص�</param>
    /// <param name="num">��ʼ����</param>
    /// <param name="maxValue">��������ֵ</param>
    /// <param name="autoCorrectMax">�������ֵ���Ƿ��Զ�����</param>
    /// <param name="minValue">�������Сֵ</param>
    /// <param name="autoCorrectMin">������Сֵ���Ƿ��Զ�����</param>
    /// <returns>�½��ĺ��</returns>
    public RDNode CreateRDNode(string name, string fatherName, Action<OverKind, int> action = null, int num = 0, int maxValue = 99, bool autoCorrectMax = false, int minValue = 0,  bool autoCorrectMin = true) 
    {
        if(!RDNodeDic.ContainsKey(name))
        {
            RDNode newRDNode = new RDNode();
            newRDNode.num = num;
            if (fatherName == "" || fatherName == null)
                newRDNode.Father = null;
            else
                newRDNode.Father = RDNodeDic[fatherName];
            newRDNode.changeAction = action;
            newRDNode.maxValue = maxValue;
            newRDNode.autoCorrectMax = autoCorrectMax;
            newRDNode.minValue = minValue;
            newRDNode.autoCorrectMin = autoCorrectMin;
            RDNodeDic.Add(name, newRDNode);

            return newRDNode;
        }
        else
        {
            Debug.LogError($"RedDot Node '{name}' has been exist.");
            return RDNodeDic[name];
        }
    }
    
    /// <summary>
    /// �½�����ӣ�һ�����
    /// </summary>
    /// <param name="name">�������</param>
    /// <param name="newRDNode">Ҫ��ӵĺ��</param>
    public void CreateRDNode(string name, RDNode newRDNode)
    {
        if (!RDNodeDic.ContainsKey(name))
            Debug.LogError($"RedDot Node '{name}' has been exist.");
        else
            RDNodeDic.Add(name, newRDNode);
    }

    //====================================================================================================================================//

    /// <summary>
    /// ɾ��ĳ�����
    /// </summary>
    /// <param name="name">�������</param>
    public void DeleteRDNode(string name)
    {
        if (RDNodeDic.ContainsKey(name))
            RDNodeDic.Remove(name);
        else
            Debug.LogError($"RedDot Node '{name}' is not exist.");
    }

    /// <summary>
    /// ɾ��ĳ�����2
    /// </summary>
    /// <param name="newRDNode">Ŀ����</param>
    public void DeleteRDNode(RDNode newRDNode)
    {
        if (RDNodeDic.ContainsValue(newRDNode))
        {
            List<string> keysToRemove = new List<string>();
            foreach (string key in  RDNodeDic.Keys)
            {
                if (RDNodeDic[key] == newRDNode) 
                    keysToRemove.Add(key);
            }
            foreach (string key in keysToRemove) 
            {
                RDNodeDic.Remove(key);
            }
        }
        else
        {
            Debug.LogError($"RedDot Node is not exist.");
        }
    }

    //====================================================================================================================================//

    /// <summary>
    /// ˢ�����еĺ��
    /// </summary>
    public void RefushRDNode()
    {
        foreach(KeyValuePair<string, RDNode> item in RDNodeDic)
        {
            int num = item.Value.num;
            OverKind kind = CheckRDNodeStatus(item.Value);
            item.Value.changeAction?.Invoke(kind, num);
        }
    }

    /// <summary>
    /// ˢ��ָ�����
    /// </summary>
    /// <param name="rdNode">ָ�����</param>
    /// <param name="ifRefushF">�Ƿ�Ӱ�츸����</param>
    public void RefushRDNode(RDNode rdNode, bool ifRefushF = true)
    {
        RDNode curRDNode = rdNode;
        while (true)
        {
            int num = curRDNode.num;
            OverKind kind = CheckRDNodeStatus(curRDNode);
            curRDNode.changeAction?.Invoke(kind, num);

            if (ifRefushF && curRDNode.Father != null)
                curRDNode = curRDNode.Father;
            else
                break;
        }
    }

    /// <summary>
    /// ˢ��ָ�����
    /// </summary>
    /// <param name="name">ָ���������</param>
    /// <param name="ifRefushF">�Ƿ�Ӱ�츸����</param>
    public void RefushRDNode(string name, bool ifRefushF = true)
    {
        RefushRDNode(RDNodeDic[name], ifRefushF);
    }
}

//����ö��
public enum SetRDNodeKind
{
    Add,//��� | ����
    Remove,//�Ƴ� | ����
    Clear,//��� | ����
    Replace//�滻 | �滻
}
//�����Ŀ�б�����
public enum OverKind
{
    Overflow,//����
    InRange,//��������
    Underflow//��С
}
