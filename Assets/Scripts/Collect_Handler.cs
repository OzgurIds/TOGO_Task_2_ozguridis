
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collect_Handler : MonoBehaviour
{
    public GameObject greenpref, redpref, maintable;

    public GameObject win, lose, Joystick_Panel;
    GameObject temp;
    public int rng;
    public Material white, green;
    public List<GameObject> objs;
    int score;
    public bool handfull;
    public TextMeshProUGUI score_board;
    // Start is called before the first frame update
    void Start()
    {
        rng = Random.Range(0, 2);
        score = 0;
        handfull = false;

        //Initial generation so i can put next objects relative to first one's position
        //0 for generating green, 1 for generating red
        if (rng == 0)
        {

            temp = Instantiate<GameObject>(greenpref, maintable.transform.position + new Vector3(0.9f, 1f, -0.2f), Quaternion.identity);
            objs.Add(temp);
        }
        else
        {
            temp = Instantiate<GameObject>(redpref, maintable.transform.position + new Vector3(0.9f, 1f, -0.2f), Quaternion.identity);
            objs.Add(temp);
        }

        for (int i = 0; i < 9; i++)
        {

            rng = Random.Range(0, 2);
            if (rng == 0)
            {
                temp = Instantiate<GameObject>(greenpref, temp.transform.position + new Vector3(-0.2f, 0f, -0f), Quaternion.identity);
                objs.Add(temp);
            }
            else
            {
                temp = Instantiate<GameObject>(redpref, temp.transform.position + new Vector3(-0.2f, 0f, -0f), Quaternion.identity);
                objs.Add(temp);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        score_board.text = "Score: " + score;
        if (objs.Count == 0 && !handfull)
        {
            if (score <= 0)
            {
                lose.SetActive(true);
                Joystick_Panel.SetActive(false);
            }
            else
            {
                win.SetActive(true);
                Joystick_Panel.SetActive(false);
            }
        }
    }
    //Main object is the table where I take the spheres/cubes, collider is the player
    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Take" && !handfull)
        {
            //swap objs[rng] to temp
            rng = Random.Range(0, objs.Count);
            temp = objs[rng];
            GetComponentInChildren<SkinnedMeshRenderer>().material = objs[rng].GetComponent<MeshRenderer>().material;
            objs[rng].transform.parent = transform;
            objs[rng].transform.localPosition = new Vector3(0, 1.5f, -0.5f);
            handfull = true;
        }
        else if (other.tag == "Drop" && handfull)
        {
            if (GetComponentInChildren<MeshRenderer>().material.color == other.GetComponent<MeshRenderer>().material.color)
            {
                if (GetComponentInChildren<MeshRenderer>().material.color == green.color)
                {
                    score += 10;
                }
                else
                {
                    score += 5;
                }
                //Destroy Child
                objs[rng].transform.parent = null;
                objs[rng].SetActive(false);
                objs.RemoveAt(rng);

                //Make main char white
                GetComponentInChildren<SkinnedMeshRenderer>().material = white;
                handfull = false;
            }
            else
            {
                score -= 5;
                //Destroy Child
                objs[rng].transform.parent = null;
                objs[rng].SetActive(false);
                objs.RemoveAt(rng);

                //Make main char white
                GetComponentInChildren<SkinnedMeshRenderer>().material = white;
                handfull = false;

            }

        }

    }
}

