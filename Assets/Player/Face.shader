Shader "Custom/Face"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1,1,1,1) //前景图颜色
        _BackColor("Back Color", Color) = (1,1,1,1) //背景图颜色
        _MainTex("Main Texture", 2D) = "white" {} //前景图
        _BackTex("Back Texture", 2D) = "white" {} //背景图
        _PosX("PosX",Float) = 4
        _PosY("PosY",Float) = 4
        _Px("Px",Float) = -0.2
        _Py("Py",Float) = 0
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 100

        // ZWrite Off
        // Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _BackTex;
            float4 _MainTex_ST;
            fixed4 _MainColor;
            fixed4 _BackColor;
            float _PosX;
            float _PosY;
            float _Px;
            float _Py;

            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                //获取背景图该uv坐标的颜色
                fixed4 backCol = tex2D(_BackTex, i.texcoord) * _BackColor;
            //创建一个相对与该uv坐标进行缩放和偏移的uv坐标
            float2 uv = float2(i.texcoord.x * _PosX, i.texcoord.y * _PosY) + float2(_Px,_Py);
            //获取前景图相对于缩放和偏移的uv坐标的颜色
            fixed4 texCol = tex2D(_MainTex, uv) * _MainColor;
            //设置默认显示颜色为背景颜色
            fixed4 col = _BackColor;
            //最终显示颜色 = 背景图片颜色 * 反的前景图片透明度 + 前景图片颜色 * 前景颜色透明的
            col.rgb = backCol.rgb * (1 - texCol.a) + texCol.rgb * texCol.a;
            UNITY_APPLY_FOG(i.fogCoord, col);
            return col;
            }
            ENDCG
        }
    }
}
