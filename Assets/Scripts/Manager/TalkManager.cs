using DI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TalkManager : DIMono
{
    [Inject]
    QuestManager questManager;

    [Inject]
    ChatManager chatManager;

    [Inject]
    SelectManager selectManager;

    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    [Inject]
    SceneChanger sceneChanger;

    public GameObject ToolBar;
    public List<SceneLine> sceneLines;

    public IEnumerator StartScene(int sceneLineCode)
    {
        yield return null;
        ToolBar.GetComponent<Slide>().SlidePanelFunc();
        playData.isAction = true;
        IEnumerable<SceneLine> sceneLines = gameData.SceneLine.Where(l => l.code == sceneLineCode);

       

        foreach (var line in sceneLines)
        {
            if (playData.NeedSkip && line.LineType!= lineType.Chat)
            {
                playData.NeedSkip = false;
            }

            switch (line.LineType)
            {
                case lineType.Chat:
                    chatManager.Chat(sceneLines, line);
                    while (playData.isTypeEffectAnim)
                    {
                        yield return null;
                    }
                    while (Input.GetKeyUp(KeyCode.F) == false)
                    {
                        if (playData.NeedSkip)
                        {
                            yield return new WaitForSeconds(0.1f);
                            break;
                        }
                        yield return null;
                    }
                    yield return null;
                    break;
                case lineType.CloseChat:
                    chatManager.CloseChat();
                    break;
                case lineType.Selection:
                    selectManager.SelectSetActive(true);
                    selectManager.Select(line.intValues);
                    while (true)
                    {
                        if (playData.selectNumber != -1)
                        {
                            selectManager.SelectResultProcess(line.intValues,playData.selectNumber);
                            playData.selectNumber = -1;
                            break;
                        }
                        yield return null;
                    }
                    selectManager.SelectSetActive(false);
                    break;
                case lineType.NextEvent:

                    break;
                case lineType.Wait:
                    yield return new WaitForSeconds(float.Parse(line.content));
                    break;
                case lineType.LoadScene:
                    sceneChanger.ChangeScene(line.content, SceneChanger.LoadingScene.FadeInOut);
                    break;
                case lineType.SetSubQuest:
                    questManager.SetSubQuest(Int32.Parse(line.content));
                    break;
                case lineType.SetMainQuest:
                    questManager.SetMainQuest(Int32.Parse(line.content));
                    break;
            }
        }
        playData.isAction = false;
        ToolBar.GetComponent<Slide>().SlidePanelFunc();
    }
}
