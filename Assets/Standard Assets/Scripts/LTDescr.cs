using System;
using UnityEngine;
using UnityEngine.UI;

public class LTDescr
{
	public delegate Vector3 EaseTypeDelegate();

	public delegate void ActionMethodDelegate();

	public bool toggle;

	public bool useEstimatedTime;

	public bool useFrames;

	public bool useManualTime;

	public bool usesNormalDt;

	public bool hasInitiliazed;

	public bool hasExtraOnCompletes;

	public bool hasPhysics;

	public bool onCompleteOnRepeat;

	public bool onCompleteOnStart;

	public bool useRecursion;

	public float ratioPassed;

	public float passed;

	public float delay;

	public float time;

	public float speed;

	public float lastVal;

	private uint _id;

	public int loopCount;

	public uint counter;

	public float direction;

	public float directionLast;

	public float overshoot;

	public float period;

	public float scale;

	public bool destroyOnComplete;

	public Transform trans;

	public LTRect ltRect;

	internal Vector3 fromInternal;

	internal Vector3 toInternal;

	internal Vector3 diff;

	internal Vector3 diffDiv2;

	public TweenAction type;

	private LeanTweenType easeType;

	public LeanTweenType loopType;

	public bool hasUpdateCallback;

	public EaseTypeDelegate easeMethod;

	public SpriteRenderer spriteRen;

	public RectTransform rectTransform;

	public Text uiText;

	public Image uiImage;

	public RawImage rawImage;

	public Sprite[] sprites;

	public LTDescrOptional _optional = new LTDescrOptional();

	public static float val;

	public static float dt;

	public static Vector3 newVect;

	public Vector3 from
	{
		get
		{
			return fromInternal;
		}
		set
		{
			fromInternal = value;
		}
	}

	public Vector3 to
	{
		get
		{
			return toInternal;
		}
		set
		{
			toInternal = value;
		}
	}

	public ActionMethodDelegate easeInternal
	{
		get;
		set;
	}

	public ActionMethodDelegate initInternal
	{
		get;
		set;
	}

	public int uniqueId => (int)(_id | (counter << 16));

	public int id => uniqueId;

	public LTDescrOptional optional
	{
		get
		{
			return _optional;
		}
		set
		{
			_optional = optional;
		}
	}

	public override string ToString()
	{
		return ((!(trans != null)) ? "gameObject:null" : ("name:" + trans.gameObject.name)) + " toggle:" + toggle + " passed:" + passed + " time:" + time + " delay:" + delay + " direction:" + direction + " from:" + from + " to:" + to + " diff:" + diff + " type:" + type + " ease:" + easeType + " useEstimatedTime:" + useEstimatedTime + " id:" + id + " hasInitiliazed:" + hasInitiliazed;
	}

	[Obsolete("Use 'LeanTween.cancel( id )' instead")]
	public LTDescr cancel(GameObject gameObject)
	{
		if (gameObject == trans.gameObject)
		{
			LeanTween.removeTween((int)_id, uniqueId);
		}
		return this;
	}

	public void reset()
	{
		toggle = (useRecursion = (usesNormalDt = true));
		trans = null;
		spriteRen = null;
		passed = (delay = (lastVal = 0f));
		hasUpdateCallback = (useEstimatedTime = (useFrames = (hasInitiliazed = (onCompleteOnRepeat = (destroyOnComplete = (onCompleteOnStart = (useManualTime = (hasExtraOnCompletes = false))))))));
		easeType = LeanTweenType.linear;
		loopType = LeanTweenType.once;
		loopCount = 0;
		direction = (directionLast = (overshoot = (scale = 1f)));
		period = 0.3f;
		speed = -1f;
		easeMethod = easeLinear;
		Vector3 vector2 = from = (to = Vector3.zero);
		_optional.reset();
	}

	public LTDescr setMoveX()
	{
		type = TweenAction.MOVE_X;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 position3 = trans.position;
			reference.x = position3.x;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 vector = easeMethod();
			float x = vector.x;
			Vector3 position = trans.position;
			float y = position.y;
			Vector3 position2 = trans.position;
			transform.position = new Vector3(x, y, position2.z);
		};
		return this;
	}

	public LTDescr setMoveY()
	{
		type = TweenAction.MOVE_Y;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 position3 = trans.position;
			reference.x = position3.y;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 position = trans.position;
			float x = position.x;
			Vector3 vector = easeMethod();
			float x2 = vector.x;
			Vector3 position2 = trans.position;
			transform.position = new Vector3(x, x2, position2.z);
		};
		return this;
	}

	public LTDescr setMoveZ()
	{
		type = TweenAction.MOVE_Z;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 position3 = trans.position;
			reference.x = position3.z;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 position = trans.position;
			float x = position.x;
			Vector3 position2 = trans.position;
			float y = position2.y;
			Vector3 vector = easeMethod();
			transform.position = new Vector3(x, y, vector.x);
		};
		return this;
	}

	public LTDescr setMoveLocalX()
	{
		type = TweenAction.MOVE_LOCAL_X;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 localPosition3 = trans.localPosition;
			reference.x = localPosition3.x;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 vector = easeMethod();
			float x = vector.x;
			Vector3 localPosition = trans.localPosition;
			float y = localPosition.y;
			Vector3 localPosition2 = trans.localPosition;
			transform.localPosition = new Vector3(x, y, localPosition2.z);
		};
		return this;
	}

	public LTDescr setMoveLocalY()
	{
		type = TweenAction.MOVE_LOCAL_Y;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 localPosition3 = trans.localPosition;
			reference.x = localPosition3.y;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 localPosition = trans.localPosition;
			float x = localPosition.x;
			Vector3 vector = easeMethod();
			float x2 = vector.x;
			Vector3 localPosition2 = trans.localPosition;
			transform.localPosition = new Vector3(x, x2, localPosition2.z);
		};
		return this;
	}

	public LTDescr setMoveLocalZ()
	{
		type = TweenAction.MOVE_LOCAL_Z;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 localPosition3 = trans.localPosition;
			reference.x = localPosition3.z;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 localPosition = trans.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = trans.localPosition;
			float y = localPosition2.y;
			Vector3 vector = easeMethod();
			transform.localPosition = new Vector3(x, y, vector.x);
		};
		return this;
	}

	private void initFromInternal()
	{
		fromInternal.x = 0f;
	}

	public LTDescr setMoveCurved()
	{
		type = TweenAction.MOVE_CURVED;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.path.orientToPath)
			{
				if (_optional.path.orientToPath2d)
				{
					_optional.path.place2d(trans, val);
				}
				else
				{
					_optional.path.place(trans, val);
				}
			}
			else
			{
				trans.position = _optional.path.point(val);
			}
		};
		return this;
	}

	public LTDescr setMoveCurvedLocal()
	{
		type = TweenAction.MOVE_CURVED_LOCAL;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.path.orientToPath)
			{
				if (_optional.path.orientToPath2d)
				{
					_optional.path.placeLocal2d(trans, val);
				}
				else
				{
					_optional.path.placeLocal(trans, val);
				}
			}
			else
			{
				trans.localPosition = _optional.path.point(val);
			}
		};
		return this;
	}

	public LTDescr setMoveSpline()
	{
		type = TweenAction.MOVE_SPLINE;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.spline.orientToPath)
			{
				if (_optional.spline.orientToPath2d)
				{
					_optional.spline.place2d(trans, val);
				}
				else
				{
					_optional.spline.place(trans, val);
				}
			}
			else
			{
				trans.position = _optional.spline.point(val);
			}
		};
		return this;
	}

	public LTDescr setMoveSplineLocal()
	{
		type = TweenAction.MOVE_SPLINE_LOCAL;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.spline.orientToPath)
			{
				if (_optional.spline.orientToPath2d)
				{
					_optional.spline.placeLocal2d(trans, val);
				}
				else
				{
					_optional.spline.placeLocal(trans, val);
				}
			}
			else
			{
				trans.localPosition = _optional.spline.point(val);
			}
		};
		return this;
	}

	public LTDescr setScaleX()
	{
		type = TweenAction.SCALE_X;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 localScale3 = trans.localScale;
			reference.x = localScale3.x;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 vector = easeMethod();
			float x = vector.x;
			Vector3 localScale = trans.localScale;
			float y = localScale.y;
			Vector3 localScale2 = trans.localScale;
			transform.localScale = new Vector3(x, y, localScale2.z);
		};
		return this;
	}

	public LTDescr setScaleY()
	{
		type = TweenAction.SCALE_Y;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 localScale3 = trans.localScale;
			reference.x = localScale3.y;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 localScale = trans.localScale;
			float x = localScale.x;
			Vector3 vector = easeMethod();
			float x2 = vector.x;
			Vector3 localScale2 = trans.localScale;
			transform.localScale = new Vector3(x, x2, localScale2.z);
		};
		return this;
	}

	public LTDescr setScaleZ()
	{
		type = TweenAction.SCALE_Z;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 localScale3 = trans.localScale;
			reference.x = localScale3.z;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 localScale = trans.localScale;
			float x = localScale.x;
			Vector3 localScale2 = trans.localScale;
			float y = localScale2.y;
			Vector3 vector = easeMethod();
			transform.localScale = new Vector3(x, y, vector.x);
		};
		return this;
	}

	public LTDescr setRotateX()
	{
		type = TweenAction.ROTATE_X;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 eulerAngles3 = trans.eulerAngles;
			reference.x = eulerAngles3.x;
			toInternal.x = LeanTween.closestRot(fromInternal.x, toInternal.x);
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 vector = easeMethod();
			float x = vector.x;
			Vector3 eulerAngles = trans.eulerAngles;
			float y = eulerAngles.y;
			Vector3 eulerAngles2 = trans.eulerAngles;
			transform.eulerAngles = new Vector3(x, y, eulerAngles2.z);
		};
		return this;
	}

	public LTDescr setRotateY()
	{
		type = TweenAction.ROTATE_Y;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 eulerAngles3 = trans.eulerAngles;
			reference.x = eulerAngles3.y;
			toInternal.x = LeanTween.closestRot(fromInternal.x, toInternal.x);
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 eulerAngles = trans.eulerAngles;
			float x = eulerAngles.x;
			Vector3 vector = easeMethod();
			float x2 = vector.x;
			Vector3 eulerAngles2 = trans.eulerAngles;
			transform.eulerAngles = new Vector3(x, x2, eulerAngles2.z);
		};
		return this;
	}

	public LTDescr setRotateZ()
	{
		type = TweenAction.ROTATE_Z;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 eulerAngles3 = trans.eulerAngles;
			reference.x = eulerAngles3.z;
			toInternal.x = LeanTween.closestRot(fromInternal.x, toInternal.x);
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 eulerAngles = trans.eulerAngles;
			float x = eulerAngles.x;
			Vector3 eulerAngles2 = trans.eulerAngles;
			float y = eulerAngles2.y;
			Vector3 vector = easeMethod();
			transform.eulerAngles = new Vector3(x, y, vector.x);
		};
		return this;
	}

	public LTDescr setRotateAround()
	{
		type = TweenAction.ROTATE_AROUND;
		initInternal = delegate
		{
			fromInternal.x = 0f;
			_optional.origRotation = trans.rotation;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Vector3 localPosition = trans.localPosition;
			Vector3 point = trans.TransformPoint(_optional.point);
			trans.RotateAround(point, _optional.axis, 0f - _optional.lastVal);
			Vector3 b = localPosition - trans.localPosition;
			trans.localPosition = localPosition - b;
			trans.rotation = _optional.origRotation;
			point = trans.TransformPoint(_optional.point);
			trans.RotateAround(point, _optional.axis, val);
			_optional.lastVal = val;
		};
		return this;
	}

	public LTDescr setRotateAroundLocal()
	{
		type = TweenAction.ROTATE_AROUND_LOCAL;
		initInternal = delegate
		{
			fromInternal.x = 0f;
			_optional.origRotation = trans.localRotation;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Vector3 localPosition = trans.localPosition;
			trans.RotateAround(trans.TransformPoint(_optional.point), trans.TransformDirection(_optional.axis), 0f - _optional.lastVal);
			Vector3 b = localPosition - trans.localPosition;
			trans.localPosition = localPosition - b;
			trans.localRotation = _optional.origRotation;
			Vector3 point = trans.TransformPoint(_optional.point);
			trans.RotateAround(point, trans.TransformDirection(_optional.axis), val);
			_optional.lastVal = val;
		};
		return this;
	}

	public LTDescr setAlpha()
	{
		type = TweenAction.ALPHA;
		initInternal = delegate
		{
			SpriteRenderer component = trans.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				ref Vector3 reference = ref fromInternal;
				Color color4 = component.color;
				reference.x = color4.a;
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				ref Vector3 reference2 = ref fromInternal;
				Color color5 = trans.GetComponent<Renderer>().material.color;
				reference2.x = color5.a;
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color6 = trans.GetComponent<Renderer>().material.GetColor("_TintColor");
				fromInternal.x = color6.a;
			}
			else if (trans.childCount > 0)
			{
				foreach (Transform tran in trans)
				{
					if (tran.gameObject.GetComponent<Renderer>() != null)
					{
						Color color7 = tran.gameObject.GetComponent<Renderer>().material.color;
						fromInternal.x = color7.a;
						break;
					}
				}
			}
			easeInternal = delegate
			{
				Vector3 vector = easeMethod();
				val = vector.x;
				if (spriteRen != null)
				{
					SpriteRenderer spriteRenderer2 = spriteRen;
					Color color8 = spriteRen.color;
					float r2 = color8.r;
					Color color9 = spriteRen.color;
					float g2 = color9.g;
					Color color10 = spriteRen.color;
					spriteRenderer2.color = new Color(r2, g2, color10.b, val);
					alphaRecursiveSprite(trans, val);
				}
				else
				{
					alphaRecursive(trans, val, useRecursion);
				}
			};
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (spriteRen != null)
			{
				SpriteRenderer spriteRenderer = spriteRen;
				Color color = spriteRen.color;
				float r = color.r;
				Color color2 = spriteRen.color;
				float g = color2.g;
				Color color3 = spriteRen.color;
				spriteRenderer.color = new Color(r, g, color3.b, val);
				alphaRecursiveSprite(trans, val);
			}
			else
			{
				alphaRecursive(trans, val, useRecursion);
			}
		};
		return this;
	}

	public LTDescr setTextAlpha()
	{
		type = TweenAction.TEXT_ALPHA;
		initInternal = delegate
		{
			uiText = trans.GetComponent<Text>();
			ref Vector3 reference = ref fromInternal;
			float x;
			if (uiText != null)
			{
				Color color = uiText.color;
				x = color.a;
			}
			else
			{
				x = 1f;
			}
			reference.x = x;
		};
		easeInternal = delegate
		{
			Transform transform = trans;
			Vector3 vector = easeMethod();
			textAlphaRecursive(transform, vector.x, useRecursion);
		};
		return this;
	}

	public LTDescr setAlphaVertex()
	{
		type = TweenAction.ALPHA_VERTEX;
		initInternal = delegate
		{
			fromInternal.x = (int)trans.GetComponent<MeshFilter>().mesh.colors32[0].a;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Mesh mesh = trans.GetComponent<MeshFilter>().mesh;
			Vector3[] vertices = mesh.vertices;
			Color32[] array = new Color32[vertices.Length];
			if (array.Length == 0)
			{
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				array = new Color32[mesh.vertices.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = color;
				}
				mesh.colors32 = array;
			}
			Color32 color2 = mesh.colors32[0];
			color2 = new Color((int)color2.r, (int)color2.g, (int)color2.b, val);
			for (int j = 0; j < vertices.Length; j++)
			{
				array[j] = color2;
			}
			mesh.colors32 = array;
		};
		return this;
	}

	public LTDescr setColor()
	{
		type = TweenAction.COLOR;
		initInternal = delegate
		{
			SpriteRenderer component = trans.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				setFromColor(component.color);
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				Color color2 = trans.GetComponent<Renderer>().material.color;
				setFromColor(color2);
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color3 = trans.GetComponent<Renderer>().material.GetColor("_TintColor");
				setFromColor(color3);
			}
			else if (trans.childCount > 0)
			{
				foreach (Transform tran in trans)
				{
					if (tran.gameObject.GetComponent<Renderer>() != null)
					{
						Color color4 = tran.gameObject.GetComponent<Renderer>().material.color;
						setFromColor(color4);
						break;
					}
				}
			}
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			if (spriteRen != null)
			{
				spriteRen.color = color;
				colorRecursiveSprite(trans, color);
			}
			else if (type == TweenAction.COLOR)
			{
				colorRecursive(trans, color, useRecursion);
			}
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			else if (dt != 0f && _optional.onUpdateColorObject != null)
			{
				_optional.onUpdateColorObject(color, _optional.onUpdateParam);
			}
		};
		return this;
	}

	public LTDescr setCallbackColor()
	{
		type = TweenAction.CALLBACK_COLOR;
		initInternal = delegate
		{
			diff = new Vector3(1f, 0f, 0f);
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			if (spriteRen != null)
			{
				spriteRen.color = color;
				colorRecursiveSprite(trans, color);
			}
			else if (type == TweenAction.COLOR)
			{
				colorRecursive(trans, color, useRecursion);
			}
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			else if (dt != 0f && _optional.onUpdateColorObject != null)
			{
				_optional.onUpdateColorObject(color, _optional.onUpdateParam);
			}
		};
		return this;
	}

	public LTDescr setTextColor()
	{
		type = TweenAction.TEXT_COLOR;
		initInternal = delegate
		{
			uiText = trans.GetComponent<Text>();
			setFromColor((!(uiText != null)) ? Color.white : uiText.color);
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			uiText.color = color;
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			if (useRecursion && trans.childCount > 0)
			{
				textColorRecursive(trans, color);
			}
		};
		return this;
	}

	public LTDescr setCanvasAlpha()
	{
		type = TweenAction.CANVAS_ALPHA;
		initInternal = delegate
		{
			uiImage = trans.GetComponent<Image>();
			if (uiImage != null)
			{
				ref Vector3 reference = ref fromInternal;
				Color color3 = uiImage.color;
				reference.x = color3.a;
			}
			else
			{
				rawImage = trans.GetComponent<RawImage>();
				if (rawImage != null)
				{
					ref Vector3 reference2 = ref fromInternal;
					Color color4 = rawImage.color;
					reference2.x = color4.a;
				}
				else
				{
					fromInternal.x = 1f;
				}
			}
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (uiImage != null)
			{
				Color color = uiImage.color;
				color.a = val;
				uiImage.color = color;
			}
			else if (rawImage != null)
			{
				Color color2 = rawImage.color;
				color2.a = val;
				rawImage.color = color2;
			}
			if (useRecursion)
			{
				alphaRecursive(rectTransform, val);
				textAlphaChildrenRecursive(rectTransform, val);
			}
		};
		return this;
	}

	public LTDescr setCanvasGroupAlpha()
	{
		type = TweenAction.CANVASGROUP_ALPHA;
		initInternal = delegate
		{
			fromInternal.x = trans.GetComponent<CanvasGroup>().alpha;
		};
		easeInternal = delegate
		{
			CanvasGroup component = trans.GetComponent<CanvasGroup>();
			Vector3 vector = easeMethod();
			component.alpha = vector.x;
		};
		return this;
	}

	public LTDescr setCanvasColor()
	{
		type = TweenAction.CANVAS_COLOR;
		initInternal = delegate
		{
			uiImage = trans.GetComponent<Image>();
			if (uiImage == null)
			{
				rawImage = trans.GetComponent<RawImage>();
				setFromColor((!(rawImage != null)) ? Color.white : rawImage.color);
			}
			else
			{
				setFromColor(uiImage.color);
			}
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			if (uiImage != null)
			{
				uiImage.color = color;
			}
			else if (rawImage != null)
			{
				rawImage.color = color;
			}
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			if (useRecursion)
			{
				colorRecursive(rectTransform, color);
			}
		};
		return this;
	}

	public LTDescr setCanvasMoveX()
	{
		type = TweenAction.CANVAS_MOVE_X;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 anchoredPosition3D2 = rectTransform.anchoredPosition3D;
			reference.x = anchoredPosition3D2.x;
		};
		easeInternal = delegate
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			RectTransform obj = rectTransform;
			Vector3 vector = easeMethod();
			obj.anchoredPosition3D = new Vector3(vector.x, anchoredPosition3D.y, anchoredPosition3D.z);
		};
		return this;
	}

	public LTDescr setCanvasMoveY()
	{
		type = TweenAction.CANVAS_MOVE_Y;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 anchoredPosition3D2 = rectTransform.anchoredPosition3D;
			reference.x = anchoredPosition3D2.y;
		};
		easeInternal = delegate
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			RectTransform obj = rectTransform;
			float x = anchoredPosition3D.x;
			Vector3 vector = easeMethod();
			obj.anchoredPosition3D = new Vector3(x, vector.x, anchoredPosition3D.z);
		};
		return this;
	}

	public LTDescr setCanvasMoveZ()
	{
		type = TweenAction.CANVAS_MOVE_Z;
		initInternal = delegate
		{
			ref Vector3 reference = ref fromInternal;
			Vector3 anchoredPosition3D2 = rectTransform.anchoredPosition3D;
			reference.x = anchoredPosition3D2.z;
		};
		easeInternal = delegate
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			RectTransform obj = rectTransform;
			float x = anchoredPosition3D.x;
			float y = anchoredPosition3D.y;
			Vector3 vector = easeMethod();
			obj.anchoredPosition3D = new Vector3(x, y, vector.x);
		};
		return this;
	}

	private void initCanvasRotateAround()
	{
		lastVal = 0f;
		fromInternal.x = 0f;
		_optional.origRotation = rectTransform.rotation;
	}

	public LTDescr setCanvasRotateAround()
	{
		type = TweenAction.CANVAS_ROTATEAROUND;
		initInternal = initCanvasRotateAround;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			RectTransform rectTransform = this.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), _optional.axis, 0f - val);
			Vector3 b = localPosition - rectTransform.localPosition;
			rectTransform.localPosition = localPosition - b;
			rectTransform.rotation = _optional.origRotation;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), _optional.axis, val);
		};
		return this;
	}

	public LTDescr setCanvasRotateAroundLocal()
	{
		type = TweenAction.CANVAS_ROTATEAROUND_LOCAL;
		initInternal = initCanvasRotateAround;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			RectTransform rectTransform = this.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), rectTransform.TransformDirection(_optional.axis), 0f - val);
			Vector3 b = localPosition - rectTransform.localPosition;
			rectTransform.localPosition = localPosition - b;
			rectTransform.rotation = _optional.origRotation;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), rectTransform.TransformDirection(_optional.axis), val);
		};
		return this;
	}

	public LTDescr setCanvasPlaySprite()
	{
		type = TweenAction.CANVAS_PLAYSPRITE;
		initInternal = delegate
		{
			uiImage = trans.GetComponent<Image>();
			fromInternal.x = 0f;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			int num = (int)Mathf.Round(val);
			uiImage.sprite = sprites[num];
		};
		return this;
	}

	public LTDescr setCanvasMove()
	{
		type = TweenAction.CANVAS_MOVE;
		initInternal = delegate
		{
			fromInternal = rectTransform.anchoredPosition3D;
		};
		easeInternal = delegate
		{
			rectTransform.anchoredPosition3D = easeMethod();
		};
		return this;
	}

	public LTDescr setCanvasScale()
	{
		type = TweenAction.CANVAS_SCALE;
		initInternal = delegate
		{
			from = rectTransform.localScale;
		};
		easeInternal = delegate
		{
			rectTransform.localScale = easeMethod();
		};
		return this;
	}

	public LTDescr setCanvasSizeDelta()
	{
		type = TweenAction.CANVAS_SIZEDELTA;
		initInternal = delegate
		{
			from = rectTransform.sizeDelta;
		};
		easeInternal = delegate
		{
			rectTransform.sizeDelta = easeMethod();
		};
		return this;
	}

	private void callback()
	{
		newVect = easeMethod();
		val = newVect.x;
	}

	public LTDescr setCallback()
	{
		type = TweenAction.CALLBACK;
		initInternal = delegate
		{
		};
		easeInternal = callback;
		return this;
	}

	public LTDescr setValue3()
	{
		type = TweenAction.VALUE3;
		initInternal = delegate
		{
		};
		easeInternal = callback;
		return this;
	}

	public LTDescr setMove()
	{
		type = TweenAction.MOVE;
		initInternal = delegate
		{
			from = trans.position;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.position = newVect;
		};
		return this;
	}

	public LTDescr setMoveLocal()
	{
		type = TweenAction.MOVE_LOCAL;
		initInternal = delegate
		{
			from = trans.localPosition;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.localPosition = newVect;
		};
		return this;
	}

	public LTDescr setMoveToTransform()
	{
		type = TweenAction.MOVE_TO_TRANSFORM;
		initInternal = delegate
		{
			from = trans.position;
		};
		easeInternal = delegate
		{
			to = _optional.toTrans.position;
			diff = to - from;
			diffDiv2 = diff * 0.5f;
			newVect = easeMethod();
			trans.position = newVect;
		};
		return this;
	}

	public LTDescr setRotate()
	{
		type = TweenAction.ROTATE;
		initInternal = delegate
		{
			this.from = trans.eulerAngles;
			float x = LeanTween.closestRot(fromInternal.x, toInternal.x);
			Vector3 from = this.from;
			float y = from.y;
			Vector3 to = this.to;
			float y2 = LeanTween.closestRot(y, to.y);
			Vector3 from2 = this.from;
			float z = from2.z;
			Vector3 to2 = this.to;
			this.to = new Vector3(x, y2, LeanTween.closestRot(z, to2.z));
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.eulerAngles = newVect;
		};
		return this;
	}

	public LTDescr setRotateLocal()
	{
		type = TweenAction.ROTATE_LOCAL;
		initInternal = delegate
		{
			this.from = trans.localEulerAngles;
			float x = LeanTween.closestRot(fromInternal.x, toInternal.x);
			Vector3 from = this.from;
			float y = from.y;
			Vector3 to = this.to;
			float y2 = LeanTween.closestRot(y, to.y);
			Vector3 from2 = this.from;
			float z = from2.z;
			Vector3 to2 = this.to;
			this.to = new Vector3(x, y2, LeanTween.closestRot(z, to2.z));
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.localEulerAngles = newVect;
		};
		return this;
	}

	public LTDescr setScale()
	{
		type = TweenAction.SCALE;
		initInternal = delegate
		{
			from = trans.localScale;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.localScale = newVect;
		};
		return this;
	}

	public LTDescr setGUIMove()
	{
		type = TweenAction.GUI_MOVE;
		initInternal = delegate
		{
			from = new Vector3(_optional.ltRect.rect.x, _optional.ltRect.rect.y, 0f);
		};
		easeInternal = delegate
		{
			Vector3 vector = easeMethod();
			_optional.ltRect.rect = new Rect(vector.x, vector.y, _optional.ltRect.rect.width, _optional.ltRect.rect.height);
		};
		return this;
	}

	public LTDescr setGUIMoveMargin()
	{
		type = TweenAction.GUI_MOVE_MARGIN;
		initInternal = delegate
		{
			from = new Vector2(_optional.ltRect.margin.x, _optional.ltRect.margin.y);
		};
		easeInternal = delegate
		{
			Vector3 vector = easeMethod();
			_optional.ltRect.margin = new Vector2(vector.x, vector.y);
		};
		return this;
	}

	public LTDescr setGUIScale()
	{
		type = TweenAction.GUI_SCALE;
		initInternal = delegate
		{
			from = new Vector3(_optional.ltRect.rect.width, _optional.ltRect.rect.height, 0f);
		};
		easeInternal = delegate
		{
			Vector3 vector = easeMethod();
			_optional.ltRect.rect = new Rect(_optional.ltRect.rect.x, _optional.ltRect.rect.y, vector.x, vector.y);
		};
		return this;
	}

	public LTDescr setGUIAlpha()
	{
		type = TweenAction.GUI_ALPHA;
		initInternal = delegate
		{
			fromInternal.x = _optional.ltRect.alpha;
		};
		easeInternal = delegate
		{
			LTRect lTRect = _optional.ltRect;
			Vector3 vector = easeMethod();
			lTRect.alpha = vector.x;
		};
		return this;
	}

	public LTDescr setGUIRotate()
	{
		type = TweenAction.GUI_ROTATE;
		initInternal = delegate
		{
			if (!_optional.ltRect.rotateEnabled)
			{
				_optional.ltRect.rotateEnabled = true;
				_optional.ltRect.resetForRotation();
			}
			fromInternal.x = _optional.ltRect.rotation;
		};
		easeInternal = delegate
		{
			LTRect lTRect = _optional.ltRect;
			Vector3 vector = easeMethod();
			lTRect.rotation = vector.x;
		};
		return this;
	}

	public LTDescr setDelayedSound()
	{
		type = TweenAction.DELAYED_SOUND;
		initInternal = delegate
		{
			hasExtraOnCompletes = true;
		};
		easeInternal = callback;
		return this;
	}

	private void init()
	{
		hasInitiliazed = true;
		usesNormalDt = (!useEstimatedTime && !useManualTime && !useFrames);
		if (useFrames)
		{
			optional.initFrameCount = Time.frameCount;
		}
		if (time <= 0f)
		{
			time = Mathf.Epsilon;
		}
		initInternal();
		diff = to - from;
		diffDiv2 = diff * 0.5f;
		if (_optional.onStart != null)
		{
			_optional.onStart();
		}
		if (onCompleteOnStart)
		{
			callOnCompletes();
		}
		if (speed >= 0f)
		{
			initSpeed();
		}
	}

	private void initSpeed()
	{
		if (type == TweenAction.MOVE_CURVED || type == TweenAction.MOVE_CURVED_LOCAL)
		{
			time = _optional.path.distance / speed;
		}
		else if (type == TweenAction.MOVE_SPLINE || type == TweenAction.MOVE_SPLINE_LOCAL)
		{
			time = _optional.spline.distance / speed;
		}
		else
		{
			time = (to - from).magnitude / speed;
		}
	}

	public LTDescr updateNow()
	{
		updateInternal();
		return this;
	}

	public bool updateInternal()
	{
		float num = direction;
		if (usesNormalDt)
		{
			dt = LeanTween.dtActual;
		}
		else if (useEstimatedTime)
		{
			dt = LeanTween.dtEstimated;
		}
		else if (useFrames)
		{
			dt = ((optional.initFrameCount != 0) ? 1 : 0);
			optional.initFrameCount = Time.frameCount;
		}
		else if (useManualTime)
		{
			dt = LeanTween.dtManual;
		}
		if (delay <= 0f && num != 0f)
		{
			if (trans == null)
			{
				return true;
			}
			if (!hasInitiliazed)
			{
				init();
			}
			dt *= num;
			passed += dt;
			ratioPassed = Mathf.Clamp01(passed / time);
			easeInternal();
			if (hasUpdateCallback)
			{
				_optional.callOnUpdate(val, ratioPassed);
			}
			if ((!(num > 0f)) ? (passed <= 0f) : (passed >= time))
			{
				loopCount--;
				if (loopType == LeanTweenType.pingPong)
				{
					direction = 0f - num;
				}
				else
				{
					passed = Mathf.Epsilon;
				}
				bool flag = loopCount == 0 || loopType == LeanTweenType.once;
				if (!flag && onCompleteOnRepeat && hasExtraOnCompletes)
				{
					callOnCompletes();
				}
				return flag;
			}
		}
		else
		{
			delay -= dt;
		}
		return false;
	}

	public void callOnCompletes()
	{
		if (type == TweenAction.GUI_ROTATE)
		{
			_optional.ltRect.rotateFinished = true;
		}
		if (type == TweenAction.DELAYED_SOUND)
		{
			AudioClip clip = (AudioClip)_optional.onCompleteParam;
			Vector3 to = this.to;
			Vector3 from = this.from;
			AudioSource.PlayClipAtPoint(clip, to, from.x);
		}
		if (_optional.onComplete != null)
		{
			_optional.onComplete();
		}
		else if (_optional.onCompleteObject != null)
		{
			_optional.onCompleteObject(_optional.onCompleteParam);
		}
	}

	public LTDescr setFromColor(Color col)
	{
		from = new Vector3(0f, col.a, 0f);
		diff = new Vector3(1f, 0f, 0f);
		_optional.axis = new Vector3(col.r, col.g, col.b);
		return this;
	}

	private static void alphaRecursive(Transform transform, float val, bool useRecursion = true)
	{
		Renderer component = transform.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			Material[] materials = component.materials;
			foreach (Material material in materials)
			{
				if (material.HasProperty("_Color"))
				{
					Material material2 = material;
					Color color = material.color;
					float r = color.r;
					Color color2 = material.color;
					float g = color2.g;
					Color color3 = material.color;
					material2.color = new Color(r, g, color3.b, val);
				}
				else if (material.HasProperty("_TintColor"))
				{
					Color color4 = material.GetColor("_TintColor");
					material.SetColor("_TintColor", new Color(color4.r, color4.g, color4.b, val));
				}
			}
		}
		if (useRecursion && transform.childCount > 0)
		{
			foreach (Transform item in transform)
			{
				alphaRecursive(item, val);
			}
		}
	}

	private static void colorRecursive(Transform transform, Color toColor, bool useRecursion = true)
	{
		Renderer component = transform.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			Material[] materials = component.materials;
			foreach (Material material in materials)
			{
				material.color = toColor;
			}
		}
		if (useRecursion && transform.childCount > 0)
		{
			foreach (Transform item in transform)
			{
				colorRecursive(item, toColor);
			}
		}
	}

	private static void alphaRecursive(RectTransform rectTransform, float val, int recursiveLevel = 0)
	{
		if (rectTransform.childCount > 0)
		{
			foreach (RectTransform item in rectTransform)
			{
				MaskableGraphic component = item.GetComponent<Image>();
				if (component != null)
				{
					Color color = component.color;
					color.a = val;
					component.color = color;
				}
				else
				{
					component = item.GetComponent<RawImage>();
					if (component != null)
					{
						Color color2 = component.color;
						color2.a = val;
						component.color = color2;
					}
				}
				alphaRecursive(item, val, recursiveLevel + 1);
			}
		}
	}

	private static void alphaRecursiveSprite(Transform transform, float val)
	{
		if (transform.childCount > 0)
		{
			foreach (Transform item in transform)
			{
				SpriteRenderer component = item.GetComponent<SpriteRenderer>();
				if (component != null)
				{
					SpriteRenderer spriteRenderer = component;
					Color color = component.color;
					float r = color.r;
					Color color2 = component.color;
					float g = color2.g;
					Color color3 = component.color;
					spriteRenderer.color = new Color(r, g, color3.b, val);
				}
				alphaRecursiveSprite(item, val);
			}
		}
	}

	private static void colorRecursiveSprite(Transform transform, Color toColor)
	{
		if (transform.childCount > 0)
		{
			foreach (Transform item in transform)
			{
				SpriteRenderer component = transform.gameObject.GetComponent<SpriteRenderer>();
				if (component != null)
				{
					component.color = toColor;
				}
				colorRecursiveSprite(item, toColor);
			}
		}
	}

	private static void colorRecursive(RectTransform rectTransform, Color toColor)
	{
		if (rectTransform.childCount > 0)
		{
			foreach (RectTransform item in rectTransform)
			{
				MaskableGraphic component = item.GetComponent<Image>();
				if (component != null)
				{
					component.color = toColor;
				}
				else
				{
					component = item.GetComponent<RawImage>();
					if (component != null)
					{
						component.color = toColor;
					}
				}
				colorRecursive(item, toColor);
			}
		}
	}

	private static void textAlphaChildrenRecursive(Transform trans, float val, bool useRecursion = true)
	{
		if (useRecursion && trans.childCount > 0)
		{
			foreach (Transform tran in trans)
			{
				Text component = tran.GetComponent<Text>();
				if (component != null)
				{
					Color color = component.color;
					color.a = val;
					component.color = color;
				}
				textAlphaChildrenRecursive(tran, val);
			}
		}
	}

	private static void textAlphaRecursive(Transform trans, float val, bool useRecursion = true)
	{
		Text component = trans.GetComponent<Text>();
		if (component != null)
		{
			Color color = component.color;
			color.a = val;
			component.color = color;
		}
		if (useRecursion && trans.childCount > 0)
		{
			foreach (Transform tran in trans)
			{
				textAlphaRecursive(tran, val);
			}
		}
	}

	private static void textColorRecursive(Transform trans, Color toColor)
	{
		if (trans.childCount > 0)
		{
			foreach (Transform tran in trans)
			{
				Text component = tran.GetComponent<Text>();
				if (component != null)
				{
					component.color = toColor;
				}
				textColorRecursive(tran, toColor);
			}
		}
	}

	private static Color tweenColor(LTDescr tween, float val)
	{
		Vector3 vector = tween._optional.point - tween._optional.axis;
		Vector3 to = tween.to;
		float y = to.y;
		Vector3 from = tween.from;
		float num = y - from.y;
		Vector3 axis = tween._optional.axis;
		float r = axis.x + vector.x * val;
		Vector3 axis2 = tween._optional.axis;
		float g = axis2.y + vector.y * val;
		Vector3 axis3 = tween._optional.axis;
		float b = axis3.z + vector.z * val;
		Vector3 from2 = tween.from;
		return new Color(r, g, b, from2.y + num * val);
	}

	public LTDescr pause()
	{
		if (direction != 0f)
		{
			directionLast = direction;
			direction = 0f;
		}
		return this;
	}

	public LTDescr resume()
	{
		direction = directionLast;
		return this;
	}

	public LTDescr setAxis(Vector3 axis)
	{
		_optional.axis = axis;
		return this;
	}

	public LTDescr setDelay(float delay)
	{
		this.delay = delay;
		return this;
	}

	public LTDescr setEase(LeanTweenType easeType)
	{
		switch (easeType)
		{
		case LeanTweenType.linear:
			setEaseLinear();
			break;
		case LeanTweenType.easeOutQuad:
			setEaseOutQuad();
			break;
		case LeanTweenType.easeInQuad:
			setEaseInQuad();
			break;
		case LeanTweenType.easeInOutQuad:
			setEaseInOutQuad();
			break;
		case LeanTweenType.easeInCubic:
			setEaseInCubic();
			break;
		case LeanTweenType.easeOutCubic:
			setEaseOutCubic();
			break;
		case LeanTweenType.easeInOutCubic:
			setEaseInOutCubic();
			break;
		case LeanTweenType.easeInQuart:
			setEaseInQuart();
			break;
		case LeanTweenType.easeOutQuart:
			setEaseOutQuart();
			break;
		case LeanTweenType.easeInOutQuart:
			setEaseInOutQuart();
			break;
		case LeanTweenType.easeInQuint:
			setEaseInQuint();
			break;
		case LeanTweenType.easeOutQuint:
			setEaseOutQuint();
			break;
		case LeanTweenType.easeInOutQuint:
			setEaseInOutQuint();
			break;
		case LeanTweenType.easeInSine:
			setEaseInSine();
			break;
		case LeanTweenType.easeOutSine:
			setEaseOutSine();
			break;
		case LeanTweenType.easeInOutSine:
			setEaseInOutSine();
			break;
		case LeanTweenType.easeInExpo:
			setEaseInExpo();
			break;
		case LeanTweenType.easeOutExpo:
			setEaseOutExpo();
			break;
		case LeanTweenType.easeInOutExpo:
			setEaseInOutExpo();
			break;
		case LeanTweenType.easeInCirc:
			setEaseInCirc();
			break;
		case LeanTweenType.easeOutCirc:
			setEaseOutCirc();
			break;
		case LeanTweenType.easeInOutCirc:
			setEaseInOutCirc();
			break;
		case LeanTweenType.easeInBounce:
			setEaseInBounce();
			break;
		case LeanTweenType.easeOutBounce:
			setEaseOutBounce();
			break;
		case LeanTweenType.easeInOutBounce:
			setEaseInOutBounce();
			break;
		case LeanTweenType.easeInBack:
			setEaseInBack();
			break;
		case LeanTweenType.easeOutBack:
			setEaseOutBack();
			break;
		case LeanTweenType.easeInOutBack:
			setEaseInOutBack();
			break;
		case LeanTweenType.easeInElastic:
			setEaseInElastic();
			break;
		case LeanTweenType.easeOutElastic:
			setEaseOutElastic();
			break;
		case LeanTweenType.easeInOutElastic:
			setEaseInOutElastic();
			break;
		case LeanTweenType.punch:
			setEasePunch();
			break;
		case LeanTweenType.easeShake:
			setEaseShake();
			break;
		case LeanTweenType.easeSpring:
			setEaseSpring();
			break;
		default:
			setEaseLinear();
			break;
		}
		return this;
	}

	public LTDescr setEaseLinear()
	{
		easeType = LeanTweenType.linear;
		easeMethod = easeLinear;
		return this;
	}

	public LTDescr setEaseSpring()
	{
		easeType = LeanTweenType.easeSpring;
		easeMethod = easeSpring;
		return this;
	}

	public LTDescr setEaseInQuad()
	{
		easeType = LeanTweenType.easeInQuad;
		easeMethod = easeInQuad;
		return this;
	}

	public LTDescr setEaseOutQuad()
	{
		easeType = LeanTweenType.easeOutQuad;
		easeMethod = easeOutQuad;
		return this;
	}

	public LTDescr setEaseInOutQuad()
	{
		easeType = LeanTweenType.easeInOutQuad;
		easeMethod = easeInOutQuad;
		return this;
	}

	public LTDescr setEaseInCubic()
	{
		easeType = LeanTweenType.easeInCubic;
		easeMethod = easeInCubic;
		return this;
	}

	public LTDescr setEaseOutCubic()
	{
		easeType = LeanTweenType.easeOutCubic;
		easeMethod = easeOutCubic;
		return this;
	}

	public LTDescr setEaseInOutCubic()
	{
		easeType = LeanTweenType.easeInOutCubic;
		easeMethod = easeInOutCubic;
		return this;
	}

	public LTDescr setEaseInQuart()
	{
		easeType = LeanTweenType.easeInQuart;
		easeMethod = easeInQuart;
		return this;
	}

	public LTDescr setEaseOutQuart()
	{
		easeType = LeanTweenType.easeOutQuart;
		easeMethod = easeOutQuart;
		return this;
	}

	public LTDescr setEaseInOutQuart()
	{
		easeType = LeanTweenType.easeInOutQuart;
		easeMethod = easeInOutQuart;
		return this;
	}

	public LTDescr setEaseInQuint()
	{
		easeType = LeanTweenType.easeInQuint;
		easeMethod = easeInQuint;
		return this;
	}

	public LTDescr setEaseOutQuint()
	{
		easeType = LeanTweenType.easeOutQuint;
		easeMethod = easeOutQuint;
		return this;
	}

	public LTDescr setEaseInOutQuint()
	{
		easeType = LeanTweenType.easeInOutQuint;
		easeMethod = easeInOutQuint;
		return this;
	}

	public LTDescr setEaseInSine()
	{
		easeType = LeanTweenType.easeInSine;
		easeMethod = easeInSine;
		return this;
	}

	public LTDescr setEaseOutSine()
	{
		easeType = LeanTweenType.easeOutSine;
		easeMethod = easeOutSine;
		return this;
	}

	public LTDescr setEaseInOutSine()
	{
		easeType = LeanTweenType.easeInOutSine;
		easeMethod = easeInOutSine;
		return this;
	}

	public LTDescr setEaseInExpo()
	{
		easeType = LeanTweenType.easeInExpo;
		easeMethod = easeInExpo;
		return this;
	}

	public LTDescr setEaseOutExpo()
	{
		easeType = LeanTweenType.easeOutExpo;
		easeMethod = easeOutExpo;
		return this;
	}

	public LTDescr setEaseInOutExpo()
	{
		easeType = LeanTweenType.easeInOutExpo;
		easeMethod = easeInOutExpo;
		return this;
	}

	public LTDescr setEaseInCirc()
	{
		easeType = LeanTweenType.easeInCirc;
		easeMethod = easeInCirc;
		return this;
	}

	public LTDescr setEaseOutCirc()
	{
		easeType = LeanTweenType.easeOutCirc;
		easeMethod = easeOutCirc;
		return this;
	}

	public LTDescr setEaseInOutCirc()
	{
		easeType = LeanTweenType.easeInOutCirc;
		easeMethod = easeInOutCirc;
		return this;
	}

	public LTDescr setEaseInBounce()
	{
		easeType = LeanTweenType.easeInBounce;
		easeMethod = easeInBounce;
		return this;
	}

	public LTDescr setEaseOutBounce()
	{
		easeType = LeanTweenType.easeOutBounce;
		easeMethod = easeOutBounce;
		return this;
	}

	public LTDescr setEaseInOutBounce()
	{
		easeType = LeanTweenType.easeInOutBounce;
		easeMethod = easeInOutBounce;
		return this;
	}

	public LTDescr setEaseInBack()
	{
		easeType = LeanTweenType.easeInBack;
		easeMethod = easeInBack;
		return this;
	}

	public LTDescr setEaseOutBack()
	{
		easeType = LeanTweenType.easeOutBack;
		easeMethod = easeOutBack;
		return this;
	}

	public LTDescr setEaseInOutBack()
	{
		easeType = LeanTweenType.easeInOutBack;
		easeMethod = easeInOutBack;
		return this;
	}

	public LTDescr setEaseInElastic()
	{
		easeType = LeanTweenType.easeInElastic;
		easeMethod = easeInElastic;
		return this;
	}

	public LTDescr setEaseOutElastic()
	{
		easeType = LeanTweenType.easeOutElastic;
		easeMethod = easeOutElastic;
		return this;
	}

	public LTDescr setEaseInOutElastic()
	{
		easeType = LeanTweenType.easeInOutElastic;
		easeMethod = easeInOutElastic;
		return this;
	}

	public LTDescr setEasePunch()
	{
		_optional.animationCurve = LeanTween.punch;
		ref Vector3 reference = ref toInternal;
		Vector3 from = this.from;
		float x = from.x;
		Vector3 to = this.to;
		reference.x = x + to.x;
		easeMethod = tweenOnCurve;
		return this;
	}

	public LTDescr setEaseShake()
	{
		_optional.animationCurve = LeanTween.shake;
		ref Vector3 reference = ref toInternal;
		Vector3 from = this.from;
		float x = from.x;
		Vector3 to = this.to;
		reference.x = x + to.x;
		easeMethod = tweenOnCurve;
		return this;
	}

	private Vector3 tweenOnCurve()
	{
		Vector3 from = this.from;
		float x = from.x + diff.x * _optional.animationCurve.Evaluate(ratioPassed);
		Vector3 from2 = this.from;
		float y = from2.y + diff.y * _optional.animationCurve.Evaluate(ratioPassed);
		Vector3 from3 = this.from;
		return new Vector3(x, y, from3.z + diff.z * _optional.animationCurve.Evaluate(ratioPassed));
	}

	private Vector3 easeInOutQuad()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val *= val;
			float num = diffDiv2.x * val;
			Vector3 from = this.from;
			float x = num + from.x;
			float num2 = diffDiv2.y * val;
			Vector3 from2 = this.from;
			float y = num2 + from2.y;
			float num3 = diffDiv2.z * val;
			Vector3 from3 = this.from;
			return new Vector3(x, y, num3 + from3.z);
		}
		val = (1f - val) * (val - 3f) + 1f;
		float num4 = diffDiv2.x * val;
		Vector3 from4 = this.from;
		float x2 = num4 + from4.x;
		float num5 = diffDiv2.y * val;
		Vector3 from5 = this.from;
		float y2 = num5 + from5.y;
		float num6 = diffDiv2.z * val;
		Vector3 from6 = this.from;
		return new Vector3(x2, y2, num6 + from6.z);
	}

	private Vector3 easeInQuad()
	{
		val = ratioPassed * ratioPassed;
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutQuad()
	{
		val = ratioPassed;
		val = (0f - val) * (val - 2f);
		return diff * val + from;
	}

	private Vector3 easeLinear()
	{
		val = ratioPassed;
		Vector3 from = this.from;
		float x = from.x + diff.x * val;
		Vector3 from2 = this.from;
		float y = from2.y + diff.y * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, from3.z + diff.z * val);
	}

	private Vector3 easeSpring()
	{
		val = Mathf.Clamp01(ratioPassed);
		val = (Mathf.Sin(val * (float)Math.PI * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
		return from + diff * val;
	}

	private Vector3 easeInCubic()
	{
		val = ratioPassed * ratioPassed * ratioPassed;
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutCubic()
	{
		val = ratioPassed - 1f;
		val = val * val * val + 1f;
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInOutCubic()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = val * val * val;
			float num = diffDiv2.x * val;
			Vector3 from = this.from;
			float x = num + from.x;
			float num2 = diffDiv2.y * val;
			Vector3 from2 = this.from;
			float y = num2 + from2.y;
			float num3 = diffDiv2.z * val;
			Vector3 from3 = this.from;
			return new Vector3(x, y, num3 + from3.z);
		}
		val -= 2f;
		val = val * val * val + 2f;
		float num4 = diffDiv2.x * val;
		Vector3 from4 = this.from;
		float x2 = num4 + from4.x;
		float num5 = diffDiv2.y * val;
		Vector3 from5 = this.from;
		float y2 = num5 + from5.y;
		float num6 = diffDiv2.z * val;
		Vector3 from6 = this.from;
		return new Vector3(x2, y2, num6 + from6.z);
	}

	private Vector3 easeInQuart()
	{
		val = ratioPassed * ratioPassed * ratioPassed * ratioPassed;
		return diff * val + from;
	}

	private Vector3 easeOutQuart()
	{
		val = ratioPassed - 1f;
		val = 0f - (val * val * val * val - 1f);
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInOutQuart()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = val * val * val * val;
			float num = diffDiv2.x * val;
			Vector3 from = this.from;
			float x = num + from.x;
			float num2 = diffDiv2.y * val;
			Vector3 from2 = this.from;
			float y = num2 + from2.y;
			float num3 = diffDiv2.z * val;
			Vector3 from3 = this.from;
			return new Vector3(x, y, num3 + from3.z);
		}
		val -= 2f;
		return -diffDiv2 * (val * val * val * val - 2f) + this.from;
	}

	private Vector3 easeInQuint()
	{
		val = ratioPassed;
		val = val * val * val * val * val;
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutQuint()
	{
		val = ratioPassed - 1f;
		val = val * val * val * val * val + 1f;
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInOutQuint()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = val * val * val * val * val;
			float num = diffDiv2.x * val;
			Vector3 from = this.from;
			float x = num + from.x;
			float num2 = diffDiv2.y * val;
			Vector3 from2 = this.from;
			float y = num2 + from2.y;
			float num3 = diffDiv2.z * val;
			Vector3 from3 = this.from;
			return new Vector3(x, y, num3 + from3.z);
		}
		val -= 2f;
		val = val * val * val * val * val + 2f;
		float num4 = diffDiv2.x * val;
		Vector3 from4 = this.from;
		float x2 = num4 + from4.x;
		float num5 = diffDiv2.y * val;
		Vector3 from5 = this.from;
		float y2 = num5 + from5.y;
		float num6 = diffDiv2.z * val;
		Vector3 from6 = this.from;
		return new Vector3(x2, y2, num6 + from6.z);
	}

	private Vector3 easeInSine()
	{
		val = 0f - Mathf.Cos(ratioPassed * LeanTween.PI_DIV2);
		float num = diff.x * val + diff.x;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val + diff.y;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val + diff.z;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutSine()
	{
		val = Mathf.Sin(ratioPassed * LeanTween.PI_DIV2);
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInOutSine()
	{
		val = 0f - (Mathf.Cos((float)Math.PI * ratioPassed) - 1f);
		float num = diffDiv2.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diffDiv2.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diffDiv2.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInExpo()
	{
		val = Mathf.Pow(2f, 10f * (ratioPassed - 1f));
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutExpo()
	{
		val = 0f - Mathf.Pow(2f, -10f * ratioPassed) + 1f;
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInOutExpo()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			return diffDiv2 * Mathf.Pow(2f, 10f * (val - 1f)) + from;
		}
		val -= 1f;
		return diffDiv2 * (0f - Mathf.Pow(2f, -10f * val) + 2f) + from;
	}

	private Vector3 easeInCirc()
	{
		val = 0f - (Mathf.Sqrt(1f - ratioPassed * ratioPassed) - 1f);
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutCirc()
	{
		val = ratioPassed - 1f;
		val = Mathf.Sqrt(1f - val * val);
		float num = diff.x * val;
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y * val;
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z * val;
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeInOutCirc()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = 0f - (Mathf.Sqrt(1f - val * val) - 1f);
			float num = diffDiv2.x * val;
			Vector3 from = this.from;
			float x = num + from.x;
			float num2 = diffDiv2.y * val;
			Vector3 from2 = this.from;
			float y = num2 + from2.y;
			float num3 = diffDiv2.z * val;
			Vector3 from3 = this.from;
			return new Vector3(x, y, num3 + from3.z);
		}
		val -= 2f;
		val = Mathf.Sqrt(1f - val * val) + 1f;
		float num4 = diffDiv2.x * val;
		Vector3 from4 = this.from;
		float x2 = num4 + from4.x;
		float num5 = diffDiv2.y * val;
		Vector3 from5 = this.from;
		float y2 = num5 + from5.y;
		float num6 = diffDiv2.z * val;
		Vector3 from6 = this.from;
		return new Vector3(x2, y2, num6 + from6.z);
	}

	private Vector3 easeInBounce()
	{
		val = ratioPassed;
		val = 1f - val;
		float num = diff.x - LeanTween.easeOutBounce(0f, diff.x, val);
		Vector3 from = this.from;
		float x = num + from.x;
		float num2 = diff.y - LeanTween.easeOutBounce(0f, diff.y, val);
		Vector3 from2 = this.from;
		float y = num2 + from2.y;
		float num3 = diff.z - LeanTween.easeOutBounce(0f, diff.z, val);
		Vector3 from3 = this.from;
		return new Vector3(x, y, num3 + from3.z);
	}

	private Vector3 easeOutBounce()
	{
		val = ratioPassed;
		float num2;
		float num;
		if (val < (num = 1f - 1.75f * overshoot / 2.75f))
		{
			val = 1f / num / num * val * val;
		}
		else if (val < (num2 = 1f - 0.75f * overshoot / 2.75f))
		{
			val -= (num + num2) / 2f;
			val = 7.5625f * val * val + 1f - 0.25f * overshoot * overshoot;
		}
		else if (val < (num = 1f - 0.25f * overshoot / 2.75f))
		{
			val -= (num + num2) / 2f;
			val = 7.5625f * val * val + 1f - 0.0625f * overshoot * overshoot;
		}
		else
		{
			val -= (num + 1f) / 2f;
			val = 7.5625f * val * val + 1f - 0.015625f * overshoot * overshoot;
		}
		return diff * val + from;
	}

	private Vector3 easeInOutBounce()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			float num = LeanTween.easeInBounce(0f, diff.x, val) * 0.5f;
			Vector3 from = this.from;
			float x = num + from.x;
			float num2 = LeanTween.easeInBounce(0f, diff.y, val) * 0.5f;
			Vector3 from2 = this.from;
			float y = num2 + from2.y;
			float num3 = LeanTween.easeInBounce(0f, diff.z, val) * 0.5f;
			Vector3 from3 = this.from;
			return new Vector3(x, y, num3 + from3.z);
		}
		val -= 1f;
		float num4 = LeanTween.easeOutBounce(0f, diff.x, val) * 0.5f + diffDiv2.x;
		Vector3 from4 = this.from;
		float x2 = num4 + from4.x;
		float num5 = LeanTween.easeOutBounce(0f, diff.y, val) * 0.5f + diffDiv2.y;
		Vector3 from5 = this.from;
		float y2 = num5 + from5.y;
		float num6 = LeanTween.easeOutBounce(0f, diff.z, val) * 0.5f + diffDiv2.z;
		Vector3 from6 = this.from;
		return new Vector3(x2, y2, num6 + from6.z);
	}

	private Vector3 easeInBack()
	{
		val = ratioPassed;
		val /= 1f;
		float num = 1.70158f * overshoot;
		return diff * val * val * ((num + 1f) * val - num) + from;
	}

	private Vector3 easeOutBack()
	{
		float num = 1.70158f * overshoot;
		val = ratioPassed / 1f - 1f;
		val = val * val * ((num + 1f) * val + num) + 1f;
		return diff * val + from;
	}

	private Vector3 easeInOutBack()
	{
		float num = 1.70158f * overshoot;
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			num *= 1.525f * overshoot;
			return diffDiv2 * (val * val * ((num + 1f) * val - num)) + from;
		}
		val -= 2f;
		num *= 1.525f * overshoot;
		val = val * val * ((num + 1f) * val + num) + 2f;
		return diffDiv2 * val + from;
	}

	private Vector3 easeInElastic()
	{
		Vector3 from = this.from;
		float x = from.x;
		Vector3 to = this.to;
		float x2 = LeanTween.easeInElastic(x, to.x, ratioPassed, overshoot, period);
		Vector3 from2 = this.from;
		float y = from2.y;
		Vector3 to2 = this.to;
		float y2 = LeanTween.easeInElastic(y, to2.y, ratioPassed, overshoot, period);
		Vector3 from3 = this.from;
		float z = from3.z;
		Vector3 to3 = this.to;
		return new Vector3(x2, y2, LeanTween.easeInElastic(z, to3.z, ratioPassed, overshoot, period));
	}

	private Vector3 easeOutElastic()
	{
		Vector3 from = this.from;
		float x = from.x;
		Vector3 to = this.to;
		float x2 = LeanTween.easeOutElastic(x, to.x, ratioPassed, overshoot, period);
		Vector3 from2 = this.from;
		float y = from2.y;
		Vector3 to2 = this.to;
		float y2 = LeanTween.easeOutElastic(y, to2.y, ratioPassed, overshoot, period);
		Vector3 from3 = this.from;
		float z = from3.z;
		Vector3 to3 = this.to;
		return new Vector3(x2, y2, LeanTween.easeOutElastic(z, to3.z, ratioPassed, overshoot, period));
	}

	private Vector3 easeInOutElastic()
	{
		Vector3 from = this.from;
		float x = from.x;
		Vector3 to = this.to;
		float x2 = LeanTween.easeInOutElastic(x, to.x, ratioPassed, overshoot, period);
		Vector3 from2 = this.from;
		float y = from2.y;
		Vector3 to2 = this.to;
		float y2 = LeanTween.easeInOutElastic(y, to2.y, ratioPassed, overshoot, period);
		Vector3 from3 = this.from;
		float z = from3.z;
		Vector3 to3 = this.to;
		return new Vector3(x2, y2, LeanTween.easeInOutElastic(z, to3.z, ratioPassed, overshoot, period));
	}

	public LTDescr setOvershoot(float overshoot)
	{
		this.overshoot = overshoot;
		return this;
	}

	public LTDescr setPeriod(float period)
	{
		this.period = period;
		return this;
	}

	public LTDescr setScale(float scale)
	{
		this.scale = scale;
		return this;
	}

	public LTDescr setEase(AnimationCurve easeCurve)
	{
		_optional.animationCurve = easeCurve;
		easeMethod = tweenOnCurve;
		easeType = LeanTweenType.animationCurve;
		return this;
	}

	public LTDescr setTo(Vector3 to)
	{
		if (hasInitiliazed)
		{
			this.to = to;
			diff = to - from;
		}
		else
		{
			this.to = to;
		}
		return this;
	}

	public LTDescr setTo(Transform to)
	{
		_optional.toTrans = to;
		return this;
	}

	public LTDescr setFrom(Vector3 from)
	{
		if ((bool)trans)
		{
			init();
		}
		this.from = from;
		diff = to - this.from;
		diffDiv2 = diff * 0.5f;
		return this;
	}

	public LTDescr setFrom(float from)
	{
		return setFrom(new Vector3(from, 0f, 0f));
	}

	public LTDescr setDiff(Vector3 diff)
	{
		this.diff = diff;
		return this;
	}

	public LTDescr setHasInitialized(bool has)
	{
		hasInitiliazed = has;
		return this;
	}

	public LTDescr setId(uint id, uint global_counter)
	{
		_id = id;
		counter = global_counter;
		return this;
	}

	public LTDescr setPassed(float passed)
	{
		this.passed = passed;
		return this;
	}

	public LTDescr setTime(float time)
	{
		float num = passed / this.time;
		passed = time * num;
		this.time = time;
		return this;
	}

	public LTDescr setSpeed(float speed)
	{
		this.speed = speed;
		if (hasInitiliazed)
		{
			initSpeed();
		}
		return this;
	}

	public LTDescr setRepeat(int repeat)
	{
		loopCount = repeat;
		if ((repeat > 1 && loopType == LeanTweenType.once) || (repeat < 0 && loopType == LeanTweenType.once))
		{
			loopType = LeanTweenType.clamp;
		}
		if (type == TweenAction.CALLBACK || type == TweenAction.CALLBACK_COLOR)
		{
			setOnCompleteOnRepeat(isOn: true);
		}
		return this;
	}

	public LTDescr setLoopType(LeanTweenType loopType)
	{
		this.loopType = loopType;
		return this;
	}

	public LTDescr setUseEstimatedTime(bool useEstimatedTime)
	{
		this.useEstimatedTime = useEstimatedTime;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setIgnoreTimeScale(bool useUnScaledTime)
	{
		useEstimatedTime = useUnScaledTime;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setUseFrames(bool useFrames)
	{
		this.useFrames = useFrames;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setUseManualTime(bool useManualTime)
	{
		this.useManualTime = useManualTime;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setLoopCount(int loopCount)
	{
		loopType = LeanTweenType.clamp;
		this.loopCount = loopCount;
		return this;
	}

	public LTDescr setLoopOnce()
	{
		loopType = LeanTweenType.once;
		return this;
	}

	public LTDescr setLoopClamp()
	{
		loopType = LeanTweenType.clamp;
		if (loopCount == 0)
		{
			loopCount = -1;
		}
		return this;
	}

	public LTDescr setLoopClamp(int loops)
	{
		loopCount = loops;
		return this;
	}

	public LTDescr setLoopPingPong()
	{
		loopType = LeanTweenType.pingPong;
		if (loopCount == 0)
		{
			loopCount = -1;
		}
		return this;
	}

	public LTDescr setLoopPingPong(int loops)
	{
		loopType = LeanTweenType.pingPong;
		loopCount = ((loops != -1) ? (loops * 2) : loops);
		return this;
	}

	public LTDescr setOnComplete(Action onComplete)
	{
		_optional.onComplete = onComplete;
		hasExtraOnCompletes = true;
		return this;
	}

	public LTDescr setOnComplete(Action<object> onComplete)
	{
		_optional.onCompleteObject = onComplete;
		hasExtraOnCompletes = true;
		return this;
	}

	public LTDescr setOnComplete(Action<object> onComplete, object onCompleteParam)
	{
		_optional.onCompleteObject = onComplete;
		hasExtraOnCompletes = true;
		if (onCompleteParam != null)
		{
			_optional.onCompleteParam = onCompleteParam;
		}
		return this;
	}

	public LTDescr setOnCompleteParam(object onCompleteParam)
	{
		_optional.onCompleteParam = onCompleteParam;
		hasExtraOnCompletes = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<float> onUpdate)
	{
		_optional.onUpdateFloat = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateRatio(Action<float, float> onUpdate)
	{
		_optional.onUpdateFloatRatio = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateObject(Action<float, object> onUpdate)
	{
		_optional.onUpdateFloatObject = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateVector2(Action<Vector2> onUpdate)
	{
		_optional.onUpdateVector2 = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateVector3(Action<Vector3> onUpdate)
	{
		_optional.onUpdateVector3 = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateColor(Action<Color> onUpdate)
	{
		_optional.onUpdateColor = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateColor(Action<Color, object> onUpdate)
	{
		_optional.onUpdateColorObject = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<Color> onUpdate)
	{
		_optional.onUpdateColor = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<Color, object> onUpdate)
	{
		_optional.onUpdateColorObject = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<float, object> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateFloatObject = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector3, object> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateVector3Object = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector2> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateVector2 = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector3> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateVector3 = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdateParam(object onUpdateParam)
	{
		_optional.onUpdateParam = onUpdateParam;
		return this;
	}

	public LTDescr setOrientToPath(bool doesOrient)
	{
		if (type == TweenAction.MOVE_CURVED || type == TweenAction.MOVE_CURVED_LOCAL)
		{
			if (_optional.path == null)
			{
				_optional.path = new LTBezierPath();
			}
			_optional.path.orientToPath = doesOrient;
		}
		else
		{
			_optional.spline.orientToPath = doesOrient;
		}
		return this;
	}

	public LTDescr setOrientToPath2d(bool doesOrient2d)
	{
		setOrientToPath(doesOrient2d);
		if (type == TweenAction.MOVE_CURVED || type == TweenAction.MOVE_CURVED_LOCAL)
		{
			_optional.path.orientToPath2d = doesOrient2d;
		}
		else
		{
			_optional.spline.orientToPath2d = doesOrient2d;
		}
		return this;
	}

	public LTDescr setRect(LTRect rect)
	{
		_optional.ltRect = rect;
		return this;
	}

	public LTDescr setRect(Rect rect)
	{
		_optional.ltRect = new LTRect(rect);
		return this;
	}

	public LTDescr setPath(LTBezierPath path)
	{
		_optional.path = path;
		return this;
	}

	public LTDescr setPoint(Vector3 point)
	{
		_optional.point = point;
		return this;
	}

	public LTDescr setDestroyOnComplete(bool doesDestroy)
	{
		destroyOnComplete = doesDestroy;
		return this;
	}

	public LTDescr setAudio(object audio)
	{
		_optional.onCompleteParam = audio;
		return this;
	}

	public LTDescr setOnCompleteOnRepeat(bool isOn)
	{
		onCompleteOnRepeat = isOn;
		return this;
	}

	public LTDescr setOnCompleteOnStart(bool isOn)
	{
		onCompleteOnStart = isOn;
		return this;
	}

	public LTDescr setRect(RectTransform rect)
	{
		rectTransform = rect;
		return this;
	}

	public LTDescr setSprites(Sprite[] sprites)
	{
		this.sprites = sprites;
		return this;
	}

	public LTDescr setFrameRate(float frameRate)
	{
		time = (float)sprites.Length / frameRate;
		return this;
	}

	public LTDescr setOnStart(Action onStart)
	{
		_optional.onStart = onStart;
		return this;
	}

	public LTDescr setDirection(float direction)
	{
		if (this.direction != -1f && this.direction != 1f)
		{
			UnityEngine.Debug.LogWarning("You have passed an incorrect direction of '" + direction + "', direction must be -1f or 1f");
			return this;
		}
		if (this.direction != direction)
		{
			if (hasInitiliazed)
			{
				this.direction = direction;
			}
			else if (_optional.path != null)
			{
				_optional.path = new LTBezierPath(LTUtility.reverse(_optional.path.pts));
			}
			else if (_optional.spline != null)
			{
				_optional.spline = new LTSpline(LTUtility.reverse(_optional.spline.pts));
			}
		}
		return this;
	}

	public LTDescr setRecursive(bool useRecursion)
	{
		this.useRecursion = useRecursion;
		return this;
	}
}
