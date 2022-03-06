using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Affichage du personnage
    public SpriteRenderer playerSprite;
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    // Affichage des infos
    public Text nbFioleSoinText;
    public Text nbFioleEnergieText;
    public Text vieText;
    public Text energieText;
    public Text concentrationText;

    // Affichage des boutons
    public Button[] buttons;

    // Affichage du terrain
    public Tilemap tilemapSol;
    public Tilemap tilemapEnnemi;
    public Tilemap tilemapObjet;
    BoundsInt boundsGridSol;
    BoundsInt boundsGridEnnemi;
    BoundsInt boundsGridObjets;

    // Tuiles de bases pour l'affichage du terrain
    public Sprite wall;
    public Sprite ground;
    public Sprite ennemi;
    public Sprite fioleSoin;
    public Sprite fioleEnergie;

    // Lien vers le player controller (la logique du personnage)
    public PlayerController player;
    public int degatMonstre;

    // Affichage pour la fin du jeu
    public GameObject panelFin;
    public Text messageFin;

    public void Start()
    {
        InitPlayerController();
        degatMonstre = 10;
        buttons = GameObject.Find("PanelBoutons").GetComponentsInChildren<Button>();
    }


    private void UpdatePlayerSprite(DIRECTION direction)
    {
        switch (direction)
        {
            case DIRECTION.NORD:
                playerSprite.sprite = up;
                break;
            case DIRECTION.SUD:
                playerSprite.sprite = down;
                break;
            case DIRECTION.EST:
                playerSprite.sprite = right;
                break;
            case DIRECTION.OUEST:
                playerSprite.sprite = left;
                break;
        }
    }

    private void InitPlayerController()
    {
        boundsGridSol = tilemapSol.cellBounds;
        player.casesSol = new bool[boundsGridSol.size.x, boundsGridSol.size.y];

        boundsGridEnnemi = tilemapEnnemi.cellBounds;
        player.casesEnnemi = new bool[boundsGridEnnemi.size.x, boundsGridEnnemi.size.y];

        boundsGridObjets = tilemapObjet.cellBounds;
        player.casesObjets = new int[boundsGridObjets.size.x, boundsGridObjets.size.y];

        player.Start();
        MiseAJourAffichage();
    }

    public void ChangeCase(Vector3Int position, bool contientQuelqueChose, Sprite sQqch, Sprite sRien, Tilemap _tilemap)
    {
        Tile t = new Tile();
        t.sprite = contientQuelqueChose ? sQqch : sRien;

        _tilemap.SetTile(position, t);
    }


    public void ChargerNiveau()
    {
        for (int x = 0; x < player.casesSol.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < player.casesSol.GetUpperBound(1) + 1; y++)
            {
                Vector3Int position = new Vector3Int(x + boundsGridSol.xMin, y + boundsGridSol.yMin, 0);
                ChangeCase(position, player.casesSol[x, y], wall, ground, tilemapSol);
            }
        }
        for (int x = 0; x < player.casesEnnemi.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < player.casesEnnemi.GetUpperBound(1) + 1; y++)
            {
                Vector3Int position = new Vector3Int(x + boundsGridEnnemi.xMin, y + boundsGridEnnemi.yMin, 0);
                ChangeCase(position, player.casesEnnemi[x, y], ennemi, null, tilemapEnnemi);
            }
        }

        for (int x = 0; x < player.casesObjets.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < player.casesObjets.GetUpperBound(1) + 1; y++)
            {
                Vector3Int position = new Vector3Int(x + boundsGridObjets.xMin, y + boundsGridObjets.yMin, 0);
                Tile t = new Tile();
                t.sprite = player.casesObjets[x, y] == 1 ? fioleSoin : player.casesObjets[x, y] == 2 ? fioleEnergie : null;
                tilemapObjet.SetTile(position, t);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TourDesCreatures()
    {
        if (player.estACotéEnnemi)
        {
            player.SubitDesDegats(degatMonstre);
        }

    }



    public void JoueurACliqueAction()
    {
        TourDesCreatures();
        MiseAJourAffichage();
        if (player.estSortie)
        {
            messageFin.text = "Bravo! vous êtes sorti du labyrinthe!";
        }
        if (player.estMort)
        {
            messageFin.text = "Vous êtes mort!";
        }
        if (player.estSortie || player.estMort)
        {
            ActiverFin();
        }
    }

    private void ActiverFin()
    {
        panelFin.SetActive(true);
    }

    internal Vector3 WorldPosFromCell(int positionEnX, int positionEnY)
    {
        return tilemapSol.GetCellCenterWorld(new Vector3Int(positionEnX + boundsGridSol.xMin, positionEnY + boundsGridSol.yMin, 0));
    }

    public void Reload()
    {
        SceneManager.LoadScene("MainScene");
    }

    protected void MiseAJourAffichage()
    {
        vieText.text = player.niveauVie.ToString() + "/" + PlayerController.vieMax;
        energieText.text = player.niveauEnergie.ToString() + "/"+ PlayerController.energieMax;
        nbFioleSoinText.text = "Fioles Soin: " + player.nbFioleSoin.ToString();
        nbFioleEnergieText.text = "Fioles Energie: " + player.nbFioleEnergie.ToString();
        concentrationText.text = "Concentration:" + player.indiceConcentration.ToString();
        player.transform.position = WorldPosFromCell(player.positionEnX, player.positionEnY);
        UpdatePlayerSprite(player.direction);

        for (int x = 0; x < player.casesEnnemi.GetUpperBound(0); x++)
        {
            for (int y = 0; y < player.casesEnnemi.GetUpperBound(1); y++)
            {
                Vector3Int position = new Vector3Int(x + boundsGridEnnemi.xMin, y + boundsGridEnnemi.yMin, 0);
                Sprite sp = tilemapEnnemi.GetSprite(position);
                if (sp != null && !player.casesEnnemi[x, y])
                {
                    tilemapEnnemi.SetTile(position, new Tile());
                }
                else if (player.casesEnnemi[x, y])
                {
                    ChangeCase(position, player.casesEnnemi[x, y], ennemi, null, tilemapEnnemi);
                }
                //
                //casesEnnemi[x, y].ContientEnnemi(tilemapEnnemi.GetSprite(position).name);
            }
        }

        for (int x = 0; x < player.casesObjets.GetUpperBound(0); x++)
        {
            for (int y = 0; y < player.casesObjets.GetUpperBound(1); y++)
            {
                Vector3Int position = new Vector3Int(x + boundsGridObjets.xMin, y + boundsGridObjets.yMin, 0);
                Sprite sp = tilemapObjet.GetSprite(position);
                if (sp != null && player.casesObjets[x, y] == 0)
                {
                    tilemapObjet.SetTile(position, new Tile());
                }
                //
                //casesEnnemi[x, y].ContientEnnemi(tilemapEnnemi.GetSprite(position).name);
            }
        }

    }





}