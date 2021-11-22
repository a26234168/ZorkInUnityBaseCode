
using UnityEngine;
using Zork.Common;
using Newtonsoft.Json;
using Zork;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string ZorkGameFileAssetName = "Zork";

    [SerializeField]
    private TextMeshProUGUI MovesText;
    [SerializeField]

    private TextMeshProUGUI ScoreText;
    [SerializeField]

    private TextMeshProUGUI LocationText;
    [SerializeField]


    private UnityInputService InputService;

    [SerializeField]

    private UnityOutputService OutputService;



    void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>(ZorkGameFileAssetName);
        _game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);

        _game.Start(InputService, OutputService);

        _game.Commands["LOOK"].Action(_game);

        _game.Player.LocationChanged += Player_LocationChanged;

        _game.Player.MovesChanged += Player_MovesChanged;

        _game.Player.ScoreChanged += Player_ScoreChanged;






    }

    private void Player_LocationChanged(object sender, Room newLocation)
    {
        LocationText.text = newLocation.ToString();
    }

    private void Player_MovesChanged(object sender, int newmoves)
    {
        MovesText.text = newmoves.ToString();
    }

    private void Player_ScoreChanged(object sender, int newscore)
    {
        ScoreText.text = newscore.ToString();
    }


    private Game _game;

}

