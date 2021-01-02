using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerState initialPlayerState;
    [SerializeField] private PlayerController player;
    [SerializeField] private ShipController ship;
    private static PlayerStateManager _instance;
    public static PlayerStateManager Instance => _instance;
    private PlayerState playerState;

    private void Start()
    {
        if(_instance!=null & _instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        switch(initialPlayerState)
        {
            case PlayerState.FOOT:
                SetOnFoot(null);
                break;
            case PlayerState.SHIP:
                SetOnShip();
                break;
        }
    }

    [ContextMenu("Set On Foot")]
    public void SetOnFoot(WreckWall wall)
    {
        playerState = PlayerState.FOOT;
        player.gameObject.SetActive(true);

        // ship.Deactivate();
        player.Reactivate();

        if(wall)
        {
            player.SetStartingPosition(wall);
        }
    }

    [ContextMenu("Set On Ship")]
    public void SetOnShip()
    {
        playerState = PlayerState.SHIP;
        player.gameObject.SetActive(false);

        // ship.Reactivate();
        player.Deactivate();
    }
}

public enum PlayerState
{
    FOOT,
    SHIP
}
