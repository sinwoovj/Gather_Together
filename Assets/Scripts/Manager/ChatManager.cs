using DI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ChatManager : DIMono
{
    public Texture2D Portait;
    private string Portait_Address;

    [Inject]
    GameData gameData;
    public GameObject TalkPanel;

    public void Chat(IEnumerable<SceneLine> sceneLines, SceneLine line)
    {
        // ä�� ȭ�鿡�����ֱ�
        TalkPanel.GetComponent<Animator>().SetBool("isShow", true);
        // �ʻ�ȭ ����
        Transform TalkPanel_Portrait = TalkPanel.transform.Find("Portrait");


        Portait_Address = gameData.HostImage.Single(
           l => l.hostCode == sceneLines.FirstOrDefault().hostCode
        && l.clothCode == sceneLines.FirstOrDefault().clothCode
        && l.hostEmotion == sceneLines.FirstOrDefault().hostEmotion).assetPath;
        Debug.Log(Portait_Address);

        var porait = Addressables.LoadAssetAsync<Texture2D>(Portait_Address).WaitForCompletion();
        var rawImage = TalkPanel_Portrait.GetComponent<RawImage>();


        rawImage.texture = porait;
        // �̸� ����
        Transform TalkPanel_Name = TalkPanel.transform.Find("Name");
        Member interlocutor = gameData.Member.FirstOrDefault(l => l.Id == line.hostCode);

        if (interlocutor != null)
        {
            TalkPanel_Name.GetComponent<Text>().text = interlocutor.Name;
        }
        // ��ȭ ����
        Transform TalkPanel_Script = TalkPanel.transform.Find("Script");


        TalkPanel_Script.GetComponent<Text>().text = line.content;
    }

    internal void CloseChat()
    {
        TalkPanel.GetComponent<Animator>().SetBool("isShow", false);
    }
}
