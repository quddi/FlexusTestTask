using UI;
using UnityEngine;
using VContainer;

namespace Game
{
    public class WindowsController : MonoBehaviour
    {
        private IUiService _uiService;

        [Inject]
        private void Construct(IUiService uiService)
        {
            _uiService = uiService;
        }

        private void Start()
        {
            _uiService.TryShowWindow<GameplayWindow>(5); 
            _uiService.TryShowWindow<InputWindow>(4); 
        }
    }
}