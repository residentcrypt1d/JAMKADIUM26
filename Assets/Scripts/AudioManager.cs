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

    void ClearCatTracks()
    {
        inkSelected = false;
        charSelected = false;
        stampSelected = false;
    }

    void SetCatTrackCharcoal()
    {
        ClearCatTracks();
        charSelected = true;
    }

    void SetCatTrackStamp()
    {
        ClearCatTracks();
        stampSelected = true;
    }

    void SetCatTrackInk()
    {
        ClearCatTracks();
        inkSelected = true;
    }

    void PlaySoundClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_mouseClick");
    }

    void PlaySoundLevelClear()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_levelClear");
    }

    void PlaySoundLevelFail()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_levelFail");
    }

    void PlaySoundNeg()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_neg");
    }

    void PlaySoundPos()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_pos");
    }

    void PlaySoundStartLevel()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_startLevel");
    }

    void PlaySoundUnavailable()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/sfx_ui_unavailable");
    }

    void PlaySoundCatInk()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/catSelect/sfx_meow_ink");
    }

    void PlaySoundCatCharcoal()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/catSelect/sfx_meow_char");
    }

    void PlaySoundCatStamp()
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
