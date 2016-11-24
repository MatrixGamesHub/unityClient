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

    private Dictionary<MtxRendererService.GroundTexture, GameObject> groundMap = new Dictionary<MtxRendererService.GroundTexture, GameObject>();
    private Dictionary<MtxRendererService.WallTexture, GameObject> wallMap = new Dictionary<MtxRendererService.WallTexture, GameObject>();

    // Use this for initialization
    void Awake() {
        groundMap[MtxRendererService.GroundTexture.GRAS]     = groundGras;
        groundMap[MtxRendererService.GroundTexture.WOOD]     = groundWood;
        groundMap[MtxRendererService.GroundTexture.ROCK]     = groundRock;
        groundMap[MtxRendererService.GroundTexture.SAND]     = groundSand;
        groundMap[MtxRendererService.GroundTexture.LAVA]     = groundLava;
        groundMap[MtxRendererService.GroundTexture.SNOW]     = groundSnow;
        groundMap[MtxRendererService.GroundTexture.ICE]      = groundIce;
        groundMap[MtxRendererService.GroundTexture.EARTH]    = groundEarth;
        groundMap[MtxRendererService.GroundTexture.METAL]    = groundMetal;
        groundMap[MtxRendererService.GroundTexture.MARBLE]   = groundMarble;
        groundMap[MtxRendererService.GroundTexture.PAVEMENT] = groundPavement;
        groundMap[MtxRendererService.GroundTexture.CONCRETE] = groundConcrete;

        wallMap[MtxRendererService.WallTexture.WHITE_BRICKS] = wallWhiteBricks;
        wallMap[MtxRendererService.WallTexture.RED_BRICKS]   = wallRedBricks;
    }

    // Update is called once per frame
    void Update() {

    }

    public GameObject GetGround(MtxRendererService.GroundTexture texture) {
        if(texture == MtxRendererService.GroundTexture.NONE) {
            return null;
        }
        return groundMap[texture];

    }

    public GameObject GetWall(MtxRendererService.WallTexture texture) {
        return wallMap[texture];
    }
}
