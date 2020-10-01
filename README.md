# bunq2ynab

# Overview

This is an Azure function to automate importing of Bunq Transactions into Ynab

Microsoft offers a free plan that comes with 1 million executions per month, which is more than enough for anyone.


# Requirements

You need to have access to Bunq and Ynab Apis


# Configuration

To choose which account you want the transactions to be imported, you must edit the account and put this in Account Notes:
#bunqimport

# Environment Variables

The following environment variables are required. You can put these in your Azure function configuration or on your local during testing

* YnabApiKey - your  Ynab API Key
* YnabBudgetGuid - You can find this when you click on your budget E.g. https://app.youneedabudget.com/{BufgetGuid}/budget/202007
* BunqApiKey - your Bunq API key
* MoneyTaryAccountId - You can get this by calling Bunq's API. https://doc.bunq.com/#/monetary-account-bank/List_all_MonetaryAccountBank_for_User
* PayeeTransforms - If you want to replace payees name
