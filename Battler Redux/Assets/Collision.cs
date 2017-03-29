using UnityEngine;
using System.Collections;

public struct AABBCollider
{
    public float x1, x2, y1, y2;

    public AABBCollider(float _x1, float _x2, float _y1, float _y2)
    {
        x1 = _x1;
        x2 = _x2;
        y1 = _y1;
        y2 = _y2;
    }

    public bool CollisionX(AABBCollider _other)
    {
        return _other.x2 >= x1 && _other.x1 <= x2;
    }
    public bool CollisionY(AABBCollider _other)
    {
        return _other.y2 >= y1 && _other.y1 <= y2;
    }
    public bool Collision(AABBCollider _other)
    {
        return CollisionX(_other) && CollisionY(_other);
    }
    public bool Collision(Vector2 _other)
    {
        return _other.y >= y1 && _other.y <= y2 && _other.x >= x1 && _other.x <= x2;
    }
    public bool Collision(Vector3 _other)
    {
        return _other.y >= y1 && _other.y <= y2 && _other.x >= x1 && _other.x <= x2;
    }
}

public struct DirectedHitBox
{
    public bool right;
    public float behind, reach, width;
    public Vector2 source;

    public DirectedHitBox(Vector2 _source, bool _right, float _reach, float _width = 0.5f, float _behind = 0)
    {
        source = _source;
        right = _right;
        reach = _reach;
        width = _width;
        behind = _behind;
    }

    public AABBCollider aabb
    {
        get
        {
            AABBCollider ret = new AABBCollider();
            if (right)
            {
                ret.x1 = source.x - behind;
                ret.x2 = source.x + reach;
            }
            else
            {
                ret.x1 = source.x - reach;
                ret.x2 = source.x + behind;
            }
            ret.y1 = source.y - width;
            ret.y2 = source.y + width;

            return ret;
        }
    }
}