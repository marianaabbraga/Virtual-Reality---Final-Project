using System.Collections.Generic;
using UnityEngine;

namespace Orders
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private GameObject orderCardPrefab;
        [SerializeField] private Transform orderContainer;

        private List<GameObject> _orderCards = new List<GameObject>();

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            List<Order> activeOrders = OrderManager.Instance.GetActiveOrders();

            while (_orderCards.Count > activeOrders.Count)
            {
                Destroy(_orderCards[0]);
                _orderCards.RemoveAt(0);
            }

            while (_orderCards.Count < activeOrders.Count)
            {
                GameObject card = Instantiate(orderCardPrefab, orderContainer);
                _orderCards.Add(card);
            }

            for (int i = 0; i < activeOrders.Count; i++)
            {
                Order order = activeOrders[i];
                OrderCard card = _orderCards[i].GetComponent<OrderCard>();

                card.recipeName.text = order.Recipe.recipeName;
                card.timer.text = Mathf.CeilToInt(order.TimeRemaining) + "s";
            }
        }
    }
}