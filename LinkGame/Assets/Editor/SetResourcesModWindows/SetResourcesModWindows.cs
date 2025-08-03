using System.IO;
using UnityEditor;
using UnityEngine;

namespace XrCode
{
    public class AppConfigEditorWindow : EditorWindow
    {
        private bool useAssetBundle;
        private bool checkVersionUpdate;
        private bool loadAssetWithServer;
        private bool loadAssetWithResources;
        private string serverResourceURL = "http://49.233.134.149/ab/"; // Ĭ�Ϸ�������ַ

        [MenuItem("Tools/������Դ���ط�ʽ")]
        static void OpenWindow()
        {
            GetWindow<AppConfigEditorWindow>("������Դ���ط�ʽ");
        }

        private void OnEnable()
        {
            // ��ʼ������ʱ��ȡ��̬���ֶε�ֵ
            useAssetBundle = AppConfig.UseAssetBundle;
            checkVersionUpdate = AppConfig.CheckVersionUpdate;
            loadAssetWithServer = AppConfig.LoadAssetWithServer;
            loadAssetWithResources = AppConfig.LoadAssetWithResources;
            serverResourceURL = AppConfig.ServerResourceURL; // ��ȡ��������ַ
        }

        private void OnGUI()
        {
            GUILayout.Label("����ѡ��", EditorStyles.boldLabel);

            // �����ֶε� UI
            useAssetBundle = EditorGUILayout.Toggle("ʹ�� AssetBundle:", useAssetBundle);
            checkVersionUpdate = EditorGUILayout.Toggle("���汾����:", checkVersionUpdate);
            loadAssetWithServer = EditorGUILayout.Toggle("�ӷ�����������Դ:", loadAssetWithServer);
            // ����û����Թ�ѡ���ӷ�����������Դ������û�й�ѡ��ʹ�� AssetBundle����������ѡ
            //if (loadAssetWithServer && !useAssetBundle)
            //{
            //    loadAssetWithServer = false;
            //    EditorUtility.DisplayDialog("����", "���빴ѡʹ�� AssetBundle���ܴӷ�����������Դ��", "ȷ��");
            //}
            GUILayout.Label("ʹ�� Resources ������Դ����ʹ�� AssetBundle �ʹӷ�����������Դ������������ö�Ϊ false", GUILayout.Width(700));
            loadAssetWithResources = EditorGUILayout.Toggle("�� Resources ������Դ", loadAssetWithResources);
            //if (loadAssetWithResources)
            //{
            //    useAssetBundle = false;
            //    loadAssetWithServer = false;
            //}
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("��������Դ��ַ:", GUILayout.Width(150)); // ��ǩ���
            serverResourceURL = EditorGUILayout.TextField(serverResourceURL, GUILayout.Width(400)); // �������
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);
            GUILayout.Label("��ʾ��");
            GUILayout.Label("��ѡ�ӷ�����������Դ�����빴ѡʹ�� AssetBundle");
            GUILayout.Label("��ѡʹ�� AssetBundle�����δ��ѡ�ӷ�����������Դ����Դ��bulid��ŵ�StreamingAssetsĿ¼");
            GUILayout.Label("δ��ѡʹ�� AssetBundle��������Դ��ʽΪAssetDatabase����Դ����Asset�ļ�����");
            GUILayout.Label("���ñ��ļ���LuBan����Data�ļ��У���Resources������Ҫ������Resources�ļ��У����������StreamingAssetsĿ¼��");
            if (GUILayout.Button("��������"))
            {
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            // ���þ�̬���ֶε�ֵ
            AppConfig.UseAssetBundle = useAssetBundle;
            AppConfig.CheckVersionUpdate = checkVersionUpdate;
            AppConfig.LoadAssetWithServer = loadAssetWithServer;
            AppConfig.LoadAssetWithResources = loadAssetWithResources;
            AppConfig.ServerResourceURL = serverResourceURL; // �����������ַ
                                                             // ���浽�ļ�
            SaveToFile();

            // ˢ�±༭��
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("�����ѱ���", "������Ϣ�ѳɹ����棡", "ȷ��");
        }

        private void SaveToFile()
        {
            string path = "Assets/Scripts/Config/AppConfig.cs";
            string content = @"

using System;

public static class AppConfig
{
    public static bool UseAssetBundle = " + useAssetBundle.ToString().ToLower() + @"; // �Ƿ�ʹ�� AssetBundle
    public static bool CheckVersionUpdate = " + checkVersionUpdate.ToString().ToLower() + @"; // �Ƿ���汾����
    public static bool LoadAssetWithServer = " + loadAssetWithServer.ToString().ToLower() + @"; // �Ƿ�ӷ�����������Դ
    public static bool LoadAssetWithResources = " + loadAssetWithResources.ToString().ToLower() + @"; // �Ƿ�� Resources ������Դ
    public static  string ServerResourceURL = """ + serverResourceURL + @"""; // ��������Դ��ַ
}";

            File.WriteAllText(path, content);
        }
    }

}