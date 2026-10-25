using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public FMODUnity.StudioEventEmitter music;
    public float fadeTime = 1.0f;
    private bool inkSelected = false;
    private bool charSelected = false;
    private bool stampSelected = false;
    private float ccink = 0.0f;
    private float ccchar = 0.0f;
    private float ccstamp = 0.0f;
    private float fade;
    //private float debugTimer = 0.0f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        fade = Time.deltaTime*fadeTime;
    }

    public void ClearCatTracks()
    {
        inkSelected = false;
        charSelected = false;
        stampSelected = false;
    }

    public void SetCatTrackCharcoal()
    {
        ClearCatTracks();
        charSelected = true;
    }

    public void SetCatTrackStamp()
    {
        ClearCatTracks();
        stampSelected = true;
    }

    public void SetCatTrackInk()
    {
        ClearCatTracks();
        inkSelected = true;
    }

    public void PlaySoundClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_mouseClick");
    }

    public void PlaySoundLevelClear()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_levelClear");
    }

    public void PlaySoundLevelFail()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_levelFail");
    }

    public void PlaySoundNeg()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_neg");
    }

    public void PlaySoundPos()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_pos");
    }

    public void PlaySoundStartLevel()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_startLevel");
    }

    public void PlaySoundUnavailable()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_unavailable");
    }

    public void PlaySoundCatInk()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/catSelect/sfx_meow_ink");
    }

    public void PlaySoundCatCharcoal()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/catSelect/sfx_meow_char");
    }

    public void PlaySoundCatStamp()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/catSelect/sfx_meow_stamp");
    }

    void Update()
    {
        if (inkSelected)
        {
            ccink += 2*fade;
        }
        if (charSelected)
        {
            ccchar += 2*fade;
        }
        if (stampSelected)
        {
            ccstamp += 2*fade;
        }

        ccink = Mathf.Clamp(ccink - fade, 0.0f, 1.0f);
        ccchar = Mathf.Clamp(ccchar - fade, 0.0f, 1.0f);
        ccstamp = Mathf.Clamp(ccstamp - fade, 0.0f, 1.0f);

        music.SetParameter("inkCatSelect",ccink);
        music.SetParameter("charCatSelect",ccchar);
        music.SetParameter("stampCatSelect",ccstamp);

        /*
        debugTimer += Time.deltaTime;
        if (debugTimer >= 1.0f)
        {
            debugTimer = 0.0f;
            PlaySoundClick();
        }*/
    }
}
