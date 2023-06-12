using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using TMPro;


    public class CalibrateRoom : MonoBehaviour
    {

        private static CalibrateRoom instance;

        public static CalibrateRoom Instance { get { return instance; } }
        private enum CALIBRATION_STATE
        {
            NONE,
            DEFINE_CORNER_A,
            DEFINE_CORNER_B,
        }

        private CALIBRATION_STATE calibrationState;

        private Transform realCornerA;
        private Transform realCornerB;

        private OVRSpatialAnchor realCornerASpatial;
        private OVRSpatialAnchor realCornerBSpatial;

        private Guid guidCornerA;
        private Guid guidCornerB;

        [Header("Corners")]
        [SerializeField] private Transform virtualCornerATf;
        [SerializeField] private Transform virtualCornerBTf;

        [Header("UI Info's")]
        [SerializeField] private GameObject uiGameObject;
        [SerializeField] private TMP_Text debugText;

        [SerializeField] private OVRManager ovrManager;

        private const float calibrationTriggerTime = 3.0f;
        private float calibTriggerStartTimer;

        [SerializeField] private Transform playerRoot;

        private string debug_message;

        private static bool isTrackingAcquired = false;
        private static bool savedUUIDAvailable = false;

        private bool isSaving = false;

        private Coroutine loadingCoroutine;

        private void OnEnable()
        {
            OVRManager.TrackingAcquired += OnTrackingAcquired;
            OVRManager.TrackingLost += OnTrackingLost;
            if (OVRManager.display != null)
                OVRManager.display.RecenteredPose += OnRecenter;
        }

        private void OnDisable()
        {
            OVRManager.TrackingAcquired -= OnTrackingAcquired;
            OVRManager.TrackingLost -= OnTrackingLost;
            if(OVRManager.display != null)
                OVRManager.display.RecenteredPose -= OnRecenter;
        }

        private void OnRecenter()
        {
            if (realCornerASpatial != null && realCornerBSpatial != null)
            {
                if (realCornerASpatial.Localized && realCornerBSpatial.Localized)
                {
                    calibrationState = CALIBRATION_STATE.NONE;

                    Vector3 positionDifference = realCornerA.position - virtualCornerATf.position;

                    if (playerRoot != null)
                        playerRoot.position -= positionDifference;

                    Vector2 c2cVector = Get2DVector(realCornerA.position, realCornerB.position);
                    Vector2 c2cVirtVector = Get2DVector(virtualCornerATf.position, virtualCornerBTf.position);

                    float cornerBDistance = Vector3.Distance(realCornerB.position, virtualCornerBTf.position);

                    if (cornerBDistance > .05)
                    {
                        float angleOffsett = Vector2.SignedAngle(c2cVirtVector, c2cVector);

                        if (playerRoot != null)
                            playerRoot.RotateAround(virtualCornerATf.position, Vector3.up, angleOffsett);
                    }

                    debug_message = $"OnRecenter : Kalibrasi Selesai";
                }
            }
        }

        private void OnTrackingAcquired()
        {
            debug_message = $"OnTrackingAcquired";
            isTrackingAcquired = true;
        }

        private void OnTrackingLost()
        {
            debug_message = $"OnTrackingAcquired";
            isTrackingAcquired = false;
        }

        private void Start()
        {
            
            virtualCornerATf.gameObject.SetActive(false);
            virtualCornerBTf.gameObject.SetActive(false);

            realCornerA = new GameObject("pojok A").transform;
            realCornerA.localScale = new Vector3(0.1f, 1.0f, 0.1f);
            realCornerA.position = virtualCornerATf.position;
            realCornerB = new GameObject("pojok B").transform;
            realCornerB.localScale = new Vector3(0.1f, 1.0f, 0.1f);
            realCornerB.position = virtualCornerBTf.position;

            OVRCameraRig camRig = ovrManager.GetComponent<OVRCameraRig>();

            realCornerA.SetParent(camRig.trackingSpace, true);
            realCornerB.SetParent(camRig.trackingSpace, true);

            LoadSavedUUIDs();

            if (savedUUIDAvailable)
            {
                if(loadingCoroutine == null)
                    loadingCoroutine = StartCoroutine(LoadAnchorData());
            }
            else
            {
                calibrationState = CALIBRATION_STATE.NONE;
            }

        }

        private void LoadSavedUUIDs()
        {
            string cornerAuuidStr = PlayerPrefs.GetString("cornerA_uuid", "");
            if (cornerAuuidStr.Length > 0)
            {
                guidCornerA = new Guid(cornerAuuidStr);
            }

            string cornerBuuidStr = PlayerPrefs.GetString("cornerB_uuid", "");
            if (cornerBuuidStr.Length > 0)
            {
                guidCornerB = new Guid(cornerBuuidStr);
            }

            if (cornerAuuidStr.Length > 0 && cornerBuuidStr.Length > 0)
                savedUUIDAvailable = true;
        }

        private IEnumerator LoadAnchorData()
        {
            uiGameObject?.SetActive(true);
            debug_message = $"Waiting for TrackingAcquired...";

            yield return new WaitUntil(() => { return isTrackingAcquired; });

            OVRSpatialAnchor.LoadOptions loadOptions = new OVRSpatialAnchor.LoadOptions
            {
                Uuids = new Guid[] { guidCornerA, guidCornerB },
                MaxAnchorCount = 100,
                Timeout = 0,
                StorageLocation = OVRSpace.StorageLocation.Local
            };

            bool initiated = OVRSpatialAnchor.LoadUnboundAnchors(loadOptions, 
                (OVRSpatialAnchor.UnboundAnchor[] unbound) => {
                    foreach (OVRSpatialAnchor.UnboundAnchor anch in unbound)
                    {
                        if (anch.Localized)
                        {
                            OnLocalized(anch, true);
                        }
                        else if (!anch.Localizing)
                        {
                            anch.Localize(OnLocalized);
                        }
                    }

                });

            if (!initiated)
            {
                debug_message = $"Invalid uuids \n {guidCornerA} - {guidCornerB}";
                calibrationState = CALIBRATION_STATE.NONE;
            }
            
            
        }

        private void OnLocalized(OVRSpatialAnchor.UnboundAnchor unboundAnchor, bool success)
        {
            if (!success)
            {
                debug_message = $"Localization Gagal, Coba ulangi kalibrasi";
                calibrationState = CALIBRATION_STATE.NONE;
                return;
            }
            else
            {
                if (unboundAnchor.Uuid == guidCornerA)
                {
                    Pose pose = unboundAnchor.Pose;
                    
                    realCornerA.position = pose.position;
                    realCornerA.rotation = pose.rotation;
                    realCornerASpatial = realCornerA.gameObject.AddComponent<OVRSpatialAnchor>();
                    unboundAnchor.BindTo(realCornerASpatial);
                    realCornerA.gameObject.SetActive(true);
                    OnRecenter();
                }

                if (unboundAnchor.Uuid == guidCornerB)
                {
                    Pose pose = unboundAnchor.Pose;
                    realCornerB.position = pose.position;
                    realCornerB.rotation = pose.rotation;
                    realCornerBSpatial = realCornerB.gameObject.AddComponent<OVRSpatialAnchor>();
                    unboundAnchor.BindTo(realCornerBSpatial);
                    realCornerB.gameObject.SetActive(true);
                    OnRecenter();
                }
            }


        }

        public void StartCalibrate() {
            calibrationState = CALIBRATION_STATE.DEFINE_CORNER_A;

            debug_message = $"1. Letakkan controller kiri di \"Corner A\"\n" +
                             $"Lalu tekan \"B\" untuk melanjutkan";

            uiGameObject?.SetActive(true);

            virtualCornerATf.gameObject.SetActive(true);
            virtualCornerBTf.gameObject.SetActive(true);

            realCornerA.gameObject.SetActive(true);
            realCornerB.gameObject.SetActive(true);

            DeleteExistingAnchors();

        }

        private void DeleteExistingAnchors()
        {
            if (realCornerASpatial != null)
            {
                realCornerASpatial.Erase();
                PlayerPrefs.SetString("cornerA_uuid", "");
                Destroy(realCornerASpatial);
            }

            if (realCornerBSpatial != null)
            {
                realCornerBSpatial.Erase();
                PlayerPrefs.SetString("cornerA_uuid", "");
                Destroy(realCornerBSpatial);
            }
        }


        private void Update()
        {
            if(debugText != null)
                debugText.text = debug_message;

            if (calibrationState == CALIBRATION_STATE.NONE)
            {
                if ((OVRInput.Get(OVRInput.RawButton.X) && OVRInput.Get(OVRInput.RawButton.B)) || Input.GetKey(KeyCode.Space))
                {
                    if (calibTriggerStartTimer < calibrationTriggerTime)
                        calibTriggerStartTimer += Time.deltaTime;
                    else
                    {
                        calibTriggerStartTimer = 0;
                        StartCalibrate();
                    }
                }

                if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) &&
                    OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                {
                    if (calibTriggerStartTimer < calibrationTriggerTime)
                        calibTriggerStartTimer += Time.deltaTime;
                    else
                    {
                        calibTriggerStartTimer = 0;
                        uiGameObject?.SetActive(!uiGameObject.activeInHierarchy);
                    }
                }
            }

            switch (calibrationState)
            {
                case CALIBRATION_STATE.DEFINE_CORNER_A:

                    realCornerA.position = GetLeftControllerGlobalPosition();

                    Vector3 positionDifference = realCornerA.position - virtualCornerATf.position;

                    if (playerRoot != null)
                        playerRoot.position -= positionDifference;

                    if (OVRInput.GetDown(OVRInput.RawButton.B))
                    {
                        calibrationState = CALIBRATION_STATE.DEFINE_CORNER_B;

                        debug_message = $"2. Letakkan controller kiri di \"Corner B\"\n" +
                             $"Lalu tekan \"B\" untuk melanjutkan";
                    }
                    break;
                case CALIBRATION_STATE.DEFINE_CORNER_B:

                    realCornerB.position = GetLeftControllerGlobalPosition();

                    Vector2 c2cVector = Get2DVector(realCornerA.position, realCornerB.position);
                    Vector2 c2cVirtVector = Get2DVector(virtualCornerATf.position, virtualCornerBTf.position);

                    float angleOffsett = Vector2.SignedAngle(c2cVirtVector, c2cVector);

                    if (playerRoot != null)
                        playerRoot.RotateAround(virtualCornerATf.position, Vector3.up, angleOffsett);

                    if (OVRInput.GetDown(OVRInput.RawButton.B))
                    {
                        if (!isSaving)
                        {  

                            if (realCornerASpatial == null)
                                realCornerASpatial = realCornerA.gameObject.AddComponent<OVRSpatialAnchor>();

                            if (realCornerBSpatial == null)
                                realCornerBSpatial = realCornerB.gameObject.AddComponent<OVRSpatialAnchor>();

                            isSaving = true;

                        }

                    }

                    if (isSaving)
                    {
                        if (realCornerASpatial != null && realCornerBSpatial != null)
                        {
                            if (realCornerASpatial.Created && realCornerBSpatial.Created)
                            {
                                realCornerASpatial.Save(delegate (OVRSpatialAnchor spAnchor, bool success)
                                {
                                    debug_message = $"save {spAnchor.Uuid} {success}";
                                    if (success)
                                        PlayerPrefs.SetString("cornerA_uuid", spAnchor.Uuid.ToString());

                                    //realCornerA.gameObject.SetActive(false);
                                });
                            
                                realCornerBSpatial.Save(delegate (OVRSpatialAnchor spAnchor, bool success)
                                {
                                    debug_message = $"save {spAnchor.Uuid} {success}";
                                    if (success)
                                        PlayerPrefs.SetString("cornerB_uuid", spAnchor.Uuid.ToString());

                                    //realCornerB.gameObject.SetActive(false);
                                });

                                isSaving = false;
                                calibrationState = CALIBRATION_STATE.NONE;

                                virtualCornerATf.gameObject.SetActive(false);
                                virtualCornerBTf.gameObject.SetActive(false);
                                uiGameObject?.SetActive(false);
                            }
                        }
                    }

                    break;
            }

        }

        private Vector3 GetLeftControllerGlobalPosition()
        {
            Vector3 lControllerLocalPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Vector3 lControllerGlobalPos = ovrManager.transform.TransformPoint(lControllerLocalPos);

            return lControllerGlobalPos;
        }

        private Vector2 Get2DVector(Vector3 cornerA, Vector3 cornerB)
        {
            Vector2 cornerA2DPos = new Vector2(cornerA.x, cornerA.z);
            Vector2 cornerB2DPos = new Vector2(cornerB.x, cornerB.z);

            return cornerB2DPos - cornerA2DPos;
        }
    }

