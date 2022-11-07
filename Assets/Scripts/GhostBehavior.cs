using UnityEngine;
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }
    public void Enable()
    {
        Enable(this.duration);
    }
    public virtual void Enable(float duration)
    {
        //как я понял, когда съедаешь большую таблетку идет время, и если съедим еще одну то идет сброс времени
        this.enabled=true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
