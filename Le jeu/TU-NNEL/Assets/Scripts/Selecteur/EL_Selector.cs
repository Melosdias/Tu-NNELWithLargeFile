using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EL_Selector : MonoBehaviour
{
    Traqueur selection;                           //Instance d'une classe que j'ai crée dans un script du même dossier (cf:Traqueur).
    RaycastHit RadarCosmique;                    //Le radar cosmique!
    bool LaGlissade;                            //permet de savoir si on swipe sur les objets pour les select ou si l'on clique simplement.


    //~~~~~~~~~~~~~~~~~~~VARIABLES POUR LA POSITION DE LA SOURIS ET LES COLLIDERS (HITBOX)~~~~~~~~~~~~~~~~~~~//
    Vector3 Pos1 = new Vector3();
    Vector3 Pos2 = new Vector3();

    MeshCollider GestionDeCollision;
    Mesh LaBoite;

    Vector2[] limite;


    Vector3[] verts;
    Vector3[] vecs;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    void Start()
    {
        selection = GetComponent<Traqueur>();
        LaGlissade = false;
    }

    void Update()
    {
        //Le GetMouseButtonDown (contrairement à son frère GetMouseButton) ne retourne "true" que lors de la frame (0 dans notre cas) où le clic est maintenu
        if (Input.GetMouseButtonDown(0))
        {
            Pos1 = Input.mousePosition;
        }
        
        //Le GetMouseButton ne retourne true que si le clic est maintenu.
        if (Input.GetMouseButton(0))
        {
            if ((Pos1 - Input.mousePosition).magnitude > 40)              //magnitude = norme d'un vecteur (en l'occurence: le déplacement de la souris)
            {
                LaGlissade = true;
            }
        }

        //Lorsque le clic est relaché!
        if (Input.GetMouseButtonUp(0))
        {
            if (LaGlissade == false)
            {
                Ray RayonLaser = Camera.main.ScreenPointToRay(Pos1);

                if (Physics.Raycast(RayonLaser, out RadarCosmique, 5000.0f))
                {
                    if (Input.GetKey(KeyCode.LeftShift)) //selection inclusive
                    {
                        selection.ajouter(RadarCosmique.transform.gameObject);
                    }
                    else //selection exclusive
                    {
                        selection.tout_supprimer();
                        selection.ajouter(RadarCosmique.transform.gameObject); // Le transform, c'est un peu la hitbox attaché sur le gameobject
                    }
                }
                else     //On n'as rien select
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        //Ne fait rien (car on a press shift)
                    }
                    else
                    {
                        selection.tout_supprimer();   //supprime tout de la selection (car on a cliqué à l'exterieur)
                    }
                }
            }

            else
            {
                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;
                Pos2 = Input.mousePosition;
                limite = ObtenirLesLimites(Pos1, Pos2);

                foreach (Vector2 lim in limite)
                {
                    Ray ray = Camera.main.ScreenPointToRay(lim);

                    if (Physics.Raycast(ray, out RadarCosmique, 50000.0f, (1 << 0)))
                    {
                        verts[i] = new Vector3(RadarCosmique.point.x, RadarCosmique.point.y, RadarCosmique.point.z);
                        vecs[i] = ray.origin - RadarCosmique.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(lim), RadarCosmique.point, Color.red, 1.0f);
                    }
                    i++;
                }

                //generate the mesh
                LaBoite = generateSelectionMesh(verts, vecs);

                GestionDeCollision = gameObject.AddComponent<MeshCollider>();
                GestionDeCollision.sharedMesh = LaBoite;
                GestionDeCollision.convex = true;
                GestionDeCollision.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selection.tout_supprimer();
                }

                Destroy(GestionDeCollision, 0.02f);
            }
            LaGlissade = false;
        }
    }
    private void OnGUI()          //Faire un rectangle (j'ai tout copié collé parceke flemme)
    {
        if (LaGlissade == true)
        {
            var rect = Selecteur1.GetScreenRect(Pos1, Input.mousePosition);
            Selecteur1.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Selecteur1.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    Vector2[] ObtenirLesLimites(Vector2 Pos1, Vector2 Pos2)            //Instancie les limites de la boite
    {
        Vector2 newPos1;
        Vector2 newPos2;
        Vector2 newPos3;
        Vector2 newPos4;

        if (Pos1.x < Pos2.x) //Pos1 à Gauche de Pos2
        {
            if (Pos1.y > Pos2.y) //Si Pos1 au dessus de Pos2
            {
                newPos1 = Pos1;
                newPos2 = new Vector2(Pos2.x, Pos1.y);
                newPos3 = new Vector2(Pos1.x, Pos2.y);
                newPos4 = Pos2;
            }
            else //Pos1 au dessous de Pos 2
            {
                newPos1 = new Vector2(Pos1.x, Pos2.y);
                newPos2 = Pos2;
                newPos3 = Pos1;
                newPos4 = new Vector2(Pos2.x, Pos1.y);
            }
        }
        else //Si Pos1 est à droite de Pos2
        {
            if (Pos1.y > Pos2.y) // Si Pos1 au dessus de Pos2
            {
                newPos1 = new Vector2(Pos2.x, Pos1.y);
                newPos2 = Pos1;
                newPos3 = Pos2;
                newPos4 = new Vector2(Pos1.x, Pos2.y);
            }
            else //Pos1 au dessous de Pos 2
            {
                newPos1 = Pos2;
                newPos2 = new Vector2(Pos1.x, Pos2.y);
                newPos3 = new Vector2(Pos2.x, Pos1.y);
                newPos4 = Pos1;
            }

        }

        Vector2[] limite = { newPos1, newPos2, newPos3, newPos4 };
        return limite;

    }
    Mesh generateSelectionMesh(Vector3[] limite, Vector3[] vecs)                               //https://www.youtube.com/watch?v=OL1QgwaDsqo&list=PLROHqCOfMPkNvsMiLFNol0NqRjl9GzwFW&t=564
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = limite[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = limite[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }
    private void OnTriggerEnter(Collider other)
    {
        selection.ajouter(other.gameObject);
    }

}
