using UnityEngine;

public class GirlAnimation : MonoBehaviour
{
    private Animator girlAnimator;


    private void Awake()
    {
        girlAnimator = GetComponent<Animator>();
    }

    public void Laugh()
    {
        girlAnimator.Play("Laugh");
    }

    public void Love()
    {
        girlAnimator.Play("Love");
    }
  
}
