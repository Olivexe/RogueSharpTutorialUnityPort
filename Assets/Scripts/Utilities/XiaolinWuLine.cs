using System;
using System.Collections.Generic;
using UnityEngine;

public static class XiaolinWuLine
{

    #region Nested members

    public class Vector2IntConfidence
    {
        public readonly Vector2Int Vector2Int;
        public readonly float Confidence;

        public Vector2IntConfidence(Vector2Int _vector2Int, float _confidence)
        {
            Vector2Int = _vector2Int;
            Confidence = _confidence;
        }
    }

    public class Vector2IntConfidencePair
    {
        public readonly Vector2IntConfidence[] vectorConfidencePair = new Vector2IntConfidence[2];

        public Vector2IntConfidencePair(Vector2IntConfidence _vector2IntConfidence0,
            Vector2IntConfidence _vector2IntConfidence1)
        {
            vectorConfidencePair[0] = _vector2IntConfidence0;
            vectorConfidencePair[1] = _vector2IntConfidence1;
        }
    }

    #endregion

    #region Methods

    private static float IntPart(float _x)
    {
        return Mathf.Floor(_x);
    }

    private static float Round(float _x)
    {
        return Mathf.Round(_x);
    }

    private static float FractPart(float _x)
    {
        return _x - Mathf.Floor(_x);
    }

    private static float RfPart(float _x)
    {
        return 1 - FractPart(_x);
    }


    public static List<Vector2IntConfidencePair> CreateLine(Vector2 _start, Vector2 _end)
    {
        bool steep = Mathf.Abs(_end.y - _start.y) > Mathf.Abs(_end.x - _start.x);
        if (steep)
        {
            float temp = _start.x;
            _start.x = _start.y;
            _start.y = temp;

            temp = _end.x;
            _end.x = _end.y;
            _end.y = temp;
        }

        bool reverse = _start.x > _end.x;
        if (reverse)
        {
            float temp = _start.x;
            _start.x = _end.x;
            _end.x = temp;

            temp = _start.y;
            _start.y = _end.y;
            _end.y = temp;
        }

        float dx = _end.x - _start.x;
        float dy = _end.y - _start.y;
        float gradient = Math.Abs(dx) < float.Epsilon ? 1.0f : dy / dx;

        float xEnd = Round(_start.x);
        float yEnd = _start.y + gradient * (xEnd - _start.x);
        float xGap = RfPart(_start.x + 0.5f);

        float xPixel1 = xEnd;
        float yPixel1 = IntPart(yEnd);

        List<Vector2IntConfidencePair> vector2IntConfidencePairs = new List<Vector2IntConfidencePair>();

        if (steep)
        {
            vector2IntConfidencePairs.Add(new Vector2IntConfidencePair(
                new Vector2IntConfidence(new Vector2Int((int)yPixel1, (int)xPixel1), RfPart(yEnd) * xGap),
                new Vector2IntConfidence(new Vector2Int((int)(yPixel1 + 1.0f), (int)xPixel1), FractPart(yEnd) * xGap)));
        }
        else
        {
            vector2IntConfidencePairs.Add(new Vector2IntConfidencePair(
                new Vector2IntConfidence(new Vector2Int((int)xPixel1, (int)yPixel1), RfPart(yEnd) * xGap),
                new Vector2IntConfidence(new Vector2Int((int)xPixel1, (int)(yPixel1 + 1.0f)), FractPart(yEnd) * xGap)));
        }

        float interY = yEnd + gradient;

        xEnd = Round(_end.x);
        yEnd = _end.y + gradient * (xEnd - _end.x);
        xGap = FractPart(_end.x + 0.5f);
        float xPixel2 = xEnd;
        float yPixel2 = IntPart(yEnd);

        if (steep)
        {
            vector2IntConfidencePairs.Add(new Vector2IntConfidencePair(
                new Vector2IntConfidence(new Vector2Int((int)yPixel2, (int)xPixel2), RfPart(yEnd) * xGap),
                new Vector2IntConfidence(new Vector2Int((int)(yPixel2 + 1.0f), (int)xPixel2), FractPart(yEnd) * xGap)));
        }
        else
        {
            vector2IntConfidencePairs.Add(new Vector2IntConfidencePair(
                new Vector2IntConfidence(new Vector2Int((int)xPixel2, (int)yPixel2), RfPart(yEnd) * xGap),
                new Vector2IntConfidence(new Vector2Int((int)xPixel2, (int)(yPixel2 + 1.0f)), FractPart(yEnd) * xGap)));
        }

        if (steep)
        {
            for (int x = (int)xPixel1 + 1; x <= xPixel2 - 1; x++)
            {
                vector2IntConfidencePairs.Add(new Vector2IntConfidencePair(
                    new Vector2IntConfidence(new Vector2Int((int)IntPart(interY), x), RfPart(interY)),
                    new Vector2IntConfidence(new Vector2Int((int)(IntPart(interY) + 1.0f), x), FractPart(interY))));
                interY += gradient;
            }
        }
        else
        {
            for (int x = (int)xPixel1 + 1; x <= xPixel2 - 1; x++)
            {
                vector2IntConfidencePairs.Add(new Vector2IntConfidencePair(
                    new Vector2IntConfidence(new Vector2Int(x, (int)IntPart(interY)), RfPart(interY)),
                    new Vector2IntConfidence(new Vector2Int(x, (int)(IntPart(interY) + 1.0f)), FractPart(interY))));
                interY += gradient;
            }
        }

        if (!steep && reverse || steep && reverse)
        {
            vector2IntConfidencePairs.Reverse();
        }

        return vector2IntConfidencePairs;
    }

    #endregion

}