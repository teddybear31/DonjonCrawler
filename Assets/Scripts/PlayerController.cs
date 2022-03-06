using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // Données à mettre à jour dans les actions
    public bool estDevantMur;
    public bool estDevantEnnemi;
    public bool estACotéEnnemi;
    public bool estSurFioleEnergie;
    public bool estSurFioleDeSoin;
    public int nbFioleSoin;
    public int nbFioleEnergie;
    public int niveauEnergie;
    public int niveauVie;
    public int positionEnX;
    public int positionEnY;
    public int caseDevantX;
    public int caseDevantY;
    public DIRECTION direction;
    public bool estSortie;
    public bool estMort;
    public int indiceConcentration;

    public bool[,] casesSol;
    public bool[,] casesEnnemi;
    public int[,] casesObjets;

    // Données constantes
    public const int positionSortieX = 15;
    public const int positionSortieY = 2;
    public const int vieMax = 100;
    public const int energieMax = 100;
    public const int energieGagneParRepos = 5;
    public const int vieGagneParRepos = 5;

    public void Start()
    {
        // initialisation des données variables du personnage
        direction = DIRECTION.EST;
        positionEnX = 1;
        positionEnY = 1;
        nbFioleSoin = 0;
        nbFioleEnergie = 0;
        niveauVie = vieMax;
        niveauEnergie = energieMax;
        MiseAJourInfo();
    }


    public void MiseAJourInfo()
    {

        Debug.Log("Je suis devant un mur:" + estDevantMur);
        Debug.Log("Je suis devant un ennemi:" + estDevantEnnemi);
        Debug.Log("Je suis A COTE d'un ennemi:" + estACotéEnnemi);
        Debug.Log("Je suis sur une fiole de soin:" + estSurFioleDeSoin);
        Debug.Log("Je suis sur une fiole d'energie:" + estSurFioleEnergie);
    }

    private bool ControlerSiMort()
    {
        return false;
    }

    private bool ControlerSiSortie()
    {
        return false;
    }

    private bool ControlerSiSurFioleEnergie()
    {
        return false;
    }

    private bool ControlerSiSurFioleDeSoin()
    {
        return false;
    }

    private bool ControlerSiACoteEnnemi()
    {
        return false;
    }

    public bool ControlerConditionVictoire()
    {
        return false;
    }

    private bool ControlerSiDevantEnnemi()
    {
        return false;
    }

    private bool ControlerSiDevantMur()
    {
        return false;
    }

    public void MiseAJourCaseDevant()
    {
        
    }

    public void BoutonAvancer()
    {
        
        MiseAJourInfo();
    }

    public void BoutonBondAvant()
    {
        
        MiseAJourInfo();
    }

    public void BoutonBondArriere()
    {
            
    }

    public void BoutonTournerDroite()
    {
        
        MiseAJourInfo();
    }

    public void BoutonTournerGauche()
    {
        
        MiseAJourInfo();
    }

    public void BoutonPasDeCoteGauche()
    {
        MiseAJourInfo();
    }

    public void BoutonPasDeCoteDroit()
    {

        MiseAJourInfo();
    }

    public void BoutonRepos()
    {
        MiseAJourInfo();
    }

    public void BoutonRamasser()
    {
        MiseAJourInfo();
    }

    public void BoutonConsommerSoin()
    {

        MiseAJourInfo();
    }

    public void BoutonConsommerEnergie()
    {

        MiseAJourInfo();
    }

    public void BoutonAttaquer()
    {

        MiseAJourInfo();
    }

    public void BoutonConcentration()
    {
    }

    public void BoutonAttaqueCharge()
    {
       
        MiseAJourInfo();
    }

    public void ChargerNiveau()
    {
        
    }

    public void SubitDesDegats(int degatsSubit)
    {
        MiseAJourInfo();
    }
}
