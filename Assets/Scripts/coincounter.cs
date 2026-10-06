using UnityEngine;
using TMPro;

public class coincounter : MonoBehaviour
{
    public static coincounter Instance;

    [Header("Настройки")]
    [Tooltip("Текстовое поле для отображения количества разрушенных блоков")]
    [SerializeField] private TextMeshProUGUI coinText;

    [Tooltip("Формат текста. {0} будет заменено на количество разрушенных блоков")]
    [SerializeField] private string textFormat = "Разрушено блоков: {0}";

    private int blocksDestroyed = 0;

    void Awake()
    {
        // Singleton паттерн - только один экземпляр счетчика
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Начинаем счет с 0
        blocksDestroyed = 0;
        UpdateCoinText();
    }

    /// <summary>
    /// Добавляет счетчик разрушенных блоков
    /// </summary>
    /// <param name="amount">Количество блоков для добавления (по умолчанию 1)</param>
    public void AddBlockDestroyed(int amount = 1)
    {
        if (amount < 0) return;

        blocksDestroyed += amount;
        UpdateCoinText();
    }

    /// <summary>
    /// Устанавливает точное количество разрушенных блоков
    /// </summary>
    /// <param name="amount">Количество блоков</param>
    public void SetBlocksDestroyed(int amount)
    {
        blocksDestroyed = Mathf.Max(0, amount);
        UpdateCoinText();
    }

    /// <summary>
    /// Возвращает текущее количество разрушенных блоков
    /// </summary>
    public int GetBlocksDestroyedCount()
    {
        return blocksDestroyed;
    }

    /// <summary>
    /// Сбрасывает счетчик разрушенных блоков на 0
    /// </summary>
    public void ResetBlocksDestroyed()
    {
        blocksDestroyed = 0;
        UpdateCoinText();
    }

    /// <summary>
    /// Обновляет текст на экране
    /// </summary>
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = string.Format(textFormat, blocksDestroyed);
        }
        else
        {
            Debug.LogWarning("CoinText не назначен в инспекторе! Назначьте TextMeshProUGUI компонент.");
        }
    }
}