using UnityEngine;

namespace Mechanics
{
    public class SwitchSpriteByEnable : MonoBehaviour
    {
        [SerializeField] private Sprite _off;
        [SerializeField] private Sprite _on;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(bool enableComponent)
        {
            if (enableComponent) _spriteRenderer.sprite = _on;
            else _spriteRenderer.sprite = _off;
        }
    }
}