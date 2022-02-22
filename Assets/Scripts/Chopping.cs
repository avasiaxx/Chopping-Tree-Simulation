using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Chopping : MonoBehaviour
{
    public GameObject Tree;
    public Text TreeText;
    bool Collided = false;

    public float movementSpeed = 70;
    Vector2 movement = new Vector2();

    public Animator anim;

    /// <summary>
    /// Store Tree Animator
    /// </summary>
    private void Start()
    {
        anim  = Tree.GetComponent<Animator>();
    }
    /// <summary>
    /// If the player is near the tree & they've pressed E play the animation clip
    /// And wait for the clip to end before setting the Tree to false
    /// </summary>
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.E) && Collided)
        {
            anim.Play("ChopTree");
            StartCoroutine(AfterTreeChop());
            Collided = false;
        }
    }
    /// <summary>
    /// Waits for the clip to end before setting the Tree to false
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterTreeChop()
    {
        yield return new WaitForSeconds(2);
        Tree.SetActive(false);
    }

    /// <summary>
    /// Change the position of the player depending on input
    /// </summary>
    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2 newPosition = new Vector2(movement.x * movementSpeed * Time.deltaTime, movement.y * movementSpeed * Time.deltaTime);
        transform.Translate(newPosition);
    }
    /// <summary>
    /// If player collides with the tree, set indicator text appropriately and if they press E while colliding
    /// then play ChoppingTree animation & delete object
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            Collided = true;
            TreeText.text = "Press E to Chop";
        }
    }

    /// <summary>
    /// If player is no longer colliding with the tree, hide indicator text
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            TreeText.text = "";
        }
    }
}

