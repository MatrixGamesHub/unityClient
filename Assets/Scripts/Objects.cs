using UnityEngine;
using System.Collections.Generic;


public class Objects : MonoBehaviour {

    public GameObject player1         = null;
    public GameObject groundGras      = null;
    public GameObject groundWood      = null;
    public GameObject groundRock      = null;
    public GameObject groundSand      = null;
    public GameObject groundLava      = null;
    public GameObject groundSnow      = null;
    public GameObject groundIce       = null;
    public GameObject groundEarth     = null;
    public GameObject groundMetal     = null;
    public GameObject groundMarble    = null;
    public GameObject groundPavement  = null;
    public GameObject groundConcrete  = null;
    public GameObject wallRedBricks   = null;
    public GameObject wallWhiteBricks = null;
    public GameObject boxPrefab       = null;
    public GameObject targetPrefab    = null;
    public GameObject tilePrefab      = null;
    public GameObject pacDotPrefab    = null;
    public GameObject exitPrefab      = null;
    public GameObject keyPrefab       = null;

    private Dictionary<RendererService.GroundTexture, GameObject> groundMap = new Dictionary<RendererService.GroundTexture, GameObject>();
    private Dictionary<RendererService.WallTexture, GameObject> wallMap = new Dictionary<RendererService.WallTexture, GameObject>();

    // Use this for initialization
    void Awake() {
        groundMap[RendererService.GroundTexture.GRAS]     = groundGras;
        groundMap[RendererService.GroundTexture.WOOD]     = groundWood;
        groundMap[RendererService.GroundTexture.ROCK]     = groundRock;
        groundMap[RendererService.GroundTexture.SAND]     = groundSand;
        groundMap[RendererService.GroundTexture.LAVA]     = groundLava;
        groundMap[RendererService.GroundTexture.SNOW]     = groundSnow;
        groundMap[RendererService.GroundTexture.ICE]      = groundIce;
        groundMap[RendererService.GroundTexture.EARTH]    = groundEarth;
        groundMap[RendererService.GroundTexture.METAL]    = groundMetal;
        groundMap[RendererService.GroundTexture.MARBLE]   = groundMarble;
        groundMap[RendererService.GroundTexture.PAVEMENT] = groundPavement;
        groundMap[RendererService.GroundTexture.CONCRETE] = groundConcrete;

        wallMap[RendererService.WallTexture.WHITE_BRICKS] = wallWhiteBricks;
        wallMap[RendererService.WallTexture.RED_BRICKS]   = wallRedBricks;
    }

    // Update is called once per frame
    void Update() {

    }

    public GameObject GetGround(RendererService.GroundTexture texture) {
        if(texture == RendererService.GroundTexture.NONE) {
            return null;
        }
        return groundMap[texture];

    }

    public GameObject GetWall(RendererService.WallTexture texture) {
        return wallMap[texture];
    }
}
