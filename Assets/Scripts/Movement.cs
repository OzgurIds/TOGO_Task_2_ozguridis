using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator pc;
    public float sense = 0.001f;
    public FloatingJoystick joys;
    public GameObject joys_panel;
    public GameObject bg;
    void Start()
    {
        pc.SetBool("_isRunning", false);
        joys_panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bg.activeInHierarchy)
        {
            //Background of joystick, if its active it means character is moving
            Vector3 direction = Vector3.forward * joys.Vertical + Vector3.right * joys.Horizontal; 
            pc.SetBool("_isRunning", true);
            transform.position += new Vector3(joys.Horizontal * sense , 0f, joys.Vertical * sense );
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction), 20f * Time.deltaTime);
        }
        else
        {
            pc.SetBool("_isRunning", false);
        }
    }
}
