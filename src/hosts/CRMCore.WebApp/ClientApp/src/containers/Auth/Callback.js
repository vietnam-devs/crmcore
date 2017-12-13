import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { CallbackComponent } from 'redux-oidc';
import { userManager } from 'configs';

class Callback extends PureComponent {
  callbackSuccess = user => {
    this.props.history.push(`/`);
  };

  render() {
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={this.callbackSuccess}
        errorCallback={this.callbackSuccess}
      >
        <div>Redirecting...</div>
      </CallbackComponent>
    );
  }
}

function mapDispatchToProps(dispatch) {
  return {
    dispatch
  };
}

export default connect(null, mapDispatchToProps)(Callback);
